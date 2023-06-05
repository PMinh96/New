using AutoMapper;
using ProductManagement.Entities;
using ProductManagement.Requests.Params;
using ProductManagement.Requests.WareHouseRequest;
using ProductManagement.Responses;
using ProductManagement.Services.InterfaceService;

namespace ProductManagement.Services.Implements
{
    public class WareHouseService : IWareHouseServices
    {
        private readonly ProductManagementContext _context;
        private readonly IMapper _mapper;

        public WareHouseService(ProductManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private int ValidateManual(WareHouseAddRequest request)
        {
            var isValidName = CheckExistName(request.Name);
            if (isValidName)
            {
                return -1001;
            }
            return 1;
        }

        public int Add(WareHouseAddRequest request)
        {
            var statusCode = ValidateManual(request);
            if (statusCode < 0) return statusCode;
            var wareHouse = _mapper.Map<WareHouse>(request);
            _context.WareHouses.Add(wareHouse);
            _context.SaveChanges();
            return wareHouse.Id;
        }

        public void Delete(int id)
        {
            var found = _context.WareHouses.FirstOrDefault(x => x.Id == id);
            if (found == null)
            {
                return;
            }

            _context.WareHouses.Remove(found);
            _context.SaveChanges(true);
        }

        public WareHouseResponse FindById(int id)
        {
            var query = (from b in _context.WareHouses
                         where b.Id == id
                         select new WareHouseResponse
                         {
                             Name = b.Name,
                             Id = b.Id
                         }).FirstOrDefault();

            return query;
        }

        public FilterResponse<List<WareHouseResponse>> Search(FilterParams<WareHouseParameters> request)
        {
            #region [Vars]

            var filter = request.ObjectFilter;

            #endregion [Vars]

            #region [Build query]

            var query = (from b in _context.WareHouses
                         select new WareHouseResponse
                         {
                             Name = b.Name,
                             Id = b.Id,
                       
                         });

            #endregion [Build query]

            #region [Filter]

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(x => x.Name == filter.Name);
            }

            if (filter.Id.HasValue)
            {
                query = query.Where(x => x.Id == filter.Id);
            }

            #endregion [Filter]

            #region [Paging]

            var queryPaging = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);

            #endregion [Paging]

            var totalRecords = query.Count();
            return new FilterResponse<List<WareHouseResponse>>
            {
                TotalRecords = query.Count(),
                Data = queryPaging.ToList()
            };
        }

        public int Update(WareHouseUpdateRequest request)
        {
            {
                var found = _context.WareHouses.FirstOrDefault(x => x.Id == request.Id);
                if (found == null)
                {
                    return -1001;
                }

                found.Name = request.Name;
                found.Address = request.Address;

                _context.WareHouses.Update(found);
                return _context.SaveChanges();
            }
        }

        #region [Private func helper]

        protected bool CheckExistName(string name) => _context.WareHouses.Any(x => x.Name == name);

        public FilterResponse<List<WareHouseResponse>> ShowDataWareHouse(FilterParams<WareHouseParameters> request)
        {
            var query = (from wh in _context.WareHouses // bảng bên trái

                             #region MyRegion

                         join m in _context.MappingProductWarehouses on wh.Id equals m.WareHouseID
                                            into ms
                         from m in ms.DefaultIfEmpty()

                         join p in _context.Products on m.ProductID equals p.Id
                             into ps
                         from p in ps.DefaultIfEmpty()

                         join b in _context.Brands on p.BrandId equals b.Id
                             into bs
                         from b in bs.DefaultIfEmpty()

                             #endregion MyRegion

                         select new
                         {
                             /// Mấy bảng dùng left join thì bảng bên phải có thể null, nên phải check null trước khi truy xuất.
                             WhId = wh.Id,
                             WhName = wh.Name,
                             WhAddress = wh.Address,
                           

                             BrandId = b != null ? b.Id : 0,
                             BrandName = b != null ? b.Name : string.Empty,
                             ProductId = p != null ? b.Id : 0,
                             ProductName = p != null ? p.Name : string.Empty,
                             ProductPrice = p != null ? p.Price : 0
                            
                             // Tất cả bảng bên phải khi left join đều phải check null như BrandId
                             //ProductName = p.Name,
                             //ProductPrice = p.Price,
                             //Quantity = m.Quantity
                         });

            var groupQuery = from q in query
                             group q by new
                             {
                                 q.WhId,
                                 q.WhName,
                                 q.WhAddress
                             }
            into gq

                             orderby gq.Key.WhId
                             select new WareHouseResponse()
                             {
                                 Id = gq.Key.WhId,
                                 Name = gq.Key.WhName,
                                 Address = gq.Key.WhAddress,

                                 Products = (List<ProductResponse>)gq.Where(x => x.WhId == gq.Key.WhId).Select(x => new ProductResponse
                                 {
                                     Name = x.ProductName
                                 })
                             };

            #endregion [Private func helper]

            return new FilterResponse<List<WareHouseResponse>>
            {
                Data = groupQuery.ToList()
            };
        }
    }
}