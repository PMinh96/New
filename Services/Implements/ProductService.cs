using AutoMapper;
using ProductManagement.Entities;
using ProductManagement.Requests;
using ProductManagement.Requests.Params;
using ProductManagement.Responses;
using ProductManagement.Services.InterfaceService;

namespace ProductManagement.Services.Implements
{
    public class ProductService : IProductServices
    {
        private readonly ProductManagementContext _context;
        private readonly IMapper _mapper;

        public ProductService(ProductManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private int ValidateManual(ProductAddRequest request)
        {
            var isValidName = CheckExistName(request.Name);
            if (isValidName)
            {
                return -1001;
            }
            return 1;
        }

        public int Add(ProductAddRequest request)
        {
            var statusCode = ValidateManual(request);
            if (statusCode < 0) return statusCode;
            var product = _mapper.Map<Product>(request);
            _context.Products.Add(product);
            _context.SaveChanges();
            return product.Id;
        }

        public void Delete(int id)
        {
            var found = _context.Products.FirstOrDefault(x => x.Id == id);
            if (found == null)
            {
                return;
            }

            _context.Products.Remove(found);
            _context.SaveChanges(true);
        }

        public ProductResponse FindBy(int id)
        {
            var query = (from p in _context.Products
                         join b in _context.Brands on p.BrandId equals b.Id into bs
                         from b in bs.DefaultIfEmpty()
                         where p.Id == id
                         select new ProductResponse
                         {
                             Name = p.Name,
                             Price = p.Price,
                             Id = p.Id,
                             BrandId = b == null ? null : b.Id,
                             BrandName = b == null ? "Không xác định" : b.Name
                             
                         }).FirstOrDefault();

            return query;
        }

        public FilterResponse<List<ProductResponse>> Search(FilterParams<ProductParameters> request)
        {
            #region [Vars]

            var filter = request.ObjectFilter;

            #endregion [Vars]

            // build query (join)

            #region [Build query]

            // IQueryable: Chưa có dữ liệu - Build query
            // IEnumable
            // IList: Collect

            var query = (from p in _context.Products
                         join b in _context.Brands on p.BrandId equals b.Id into bs
                         from b in bs.DefaultIfEmpty()
                         select new ProductResponse
                         {
                             Name = p.Name,
                        
                             Price = p.Price,
                             Id = p.Id,
                             BrandId = b == null ? null : b.Id,
                             BrandName = b == null ? "Không xác định" : b.Name
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
            return new FilterResponse<List<ProductResponse>>
            {
                TotalRecords = query.Count(),
                Data = queryPaging.ToList()
            };
        }

        public int Update(ProductUpdateRequest request)
        {
            var found = _context.Products.FirstOrDefault(x => x.Id == request.Id);
            if (found == null)
            {
                return -1001;
            }

            found.Price = request.Price;
       

            _context.Products.Update(found);
            return _context.SaveChanges();
        }

        #region [Private func helper]

        protected bool CheckExistName(string name) => _context.Products.Any(x => x.Name == name);

        #endregion [Private func helper]
    }
}