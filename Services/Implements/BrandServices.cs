using AutoMapper;
using ProductManagement.Entities;
using ProductManagement.Requests.BrandRequest;
using ProductManagement.Requests.Params;
using ProductManagement.Responses;
using ProductManagement.Services.InterfaceService;

namespace ProductManagement.Services.Implements
{
    public class BrandServices : IBrandServices
    {
        private readonly ProductManagementContext _context;
        private readonly IMapper _mapper;

        public BrandServices(ProductManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private int ValidateManual(BrandAddRequet request)
        {
            var isValidName = CheckExistName(request.Name);
            if (isValidName)
            {
                return -1001;
            }
            return 1;
        }

        public int Add(BrandAddRequet request)
        {
            var statusCode = ValidateManual(request);
            if (statusCode < 0) return statusCode;
            var brand = _mapper.Map<Brand>(request);
            _context.Brands.Add(brand);
            _context.SaveChanges();
            return brand.Id;
        }

        public int Update(BrandUpdateRequest request)
        {
            var found = _context.Brands.FirstOrDefault(x => x.Id == request.Id);
            if (found == null)
            {
                return -1001;
            }

            found.Name = request.Name;

            _context.Brands.Update(found);
            return _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var found = _context.Brands.FirstOrDefault(x => x.Id == id);
            if (found == null)
            {
                return;
            }

            _context.Brands.Remove(found);
            _context.SaveChanges(true);
        }

        public BrandResponse FindById(int id)
        {
            var query = (from b in _context.Brands
                         where b.Id == id
                         select new BrandResponse
                         {
                             Name = b.Name,
                             Id = b.Id
                         }).FirstOrDefault();

            return query;
        }

        public FilterResponse<List<BrandResponse>> Search(FilterParams<BrandParameters> request)
        {
            #region [Vars]

            var filter = request.ObjectFilter;

            #endregion [Vars]

            // build query (join)

            #region [Build query]

            // IQueryable: Chưa có dữ liệu - Build query
            // IEnumable
            // IList: Collect

            var query = (from b in _context.Brands
                         select new BrandResponse
                         {
                             Name = b.Name,
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
            return new FilterResponse<List<BrandResponse>>
            {
                TotalRecords = query.Count(),
                Data = queryPaging.ToList()
            };
        }

        #region [Private func helper]

        protected bool CheckExistName(string name) => _context.Brands.Any(b => b.Name == name);

        #endregion [Private func helper]
    }
}