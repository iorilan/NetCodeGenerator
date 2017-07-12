using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using YourProject.BusinessLogic.Helpers;
using YourProject.BusinessLogic.Models;
using YourProject.DataAccess;
using YourProject.DataAccess.Models;
using System.Data.Entity.Migrations;

/// <summary>
/// This service is generated from NetGenerator
/// </summary>
namespace YourProject.BusinessLogic.Services
{
    public class yourTableObjService
    {
        private IYourProject _dbContext;
        private ILog _log = LogManager.GetLogger(typeof(yourTableObjService));

        public yourTableObjService()
        {
            _dbContext = new YourProjectContext();
        }

        public async Task<yourTableObj> GetById(int id)
        {
            try
            {
                using (var context = _dbContext.NewInstance())
                {
                    var obj = await context.yourTables
                                    .FirstOrDefaultAsync(x => x.Id == id);

                    var detail = new yourTableObj();
                    detail = MapperEx.CreateFrom<yourTable, yourTableObj(obj);
                    return detail;
                }
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error(ex.FormatException());
                return new yourTableObj();
            }
            catch (Exception ex)
            {
                _log.Error(ex.FormatException());
                return new yourTableObj();
            }
        }

        public async Task<bool> AddOrUpdate(yourTableObj obj)
        {
            try
            {
                using (var context = _dbContext.NewInstance())
                {
                    var tryFind = await context.yourTableObj
                                            .FirstOrDefaultAsync(x => x.Id == obj.Id);

                    var updateingRecord = new yourTable();
                    if (tryFind != null)
                    {
                        MapperEx.Map(obj, ref updatingRecord);
                    }
                    else
                    {
                        updatingRecord = MapperEx.CreateFrom<yourTableObj, yourTable>(obj);
                    }

                    context.yourTables.AddOrUpdate(updatingRecord);
                    await context.SaveChangesAsync();

                    return true;
                }
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error(ex.FormatException());
            }
            catch (Exception ex)
            {
                _log.Error(ex.FormatException());
            }

            return false;
        }

        
        public IList<yourTableObj> Search(out int totalCount, int start = 0, int pageSize = 10, string sortField = "", string sortDirection = "desc", string search = "")
        {
            totalCount = 0;

            try
            {
                using (var context = _dbContext.NewInstance())
                {
                    var objs = context.yourTables;

                    var records = objs.OrderByEx(sortDirection, sortField)
                                      .Skip(start).Take(pageSize).ToList();

                    totalCount = context.yourTables.Count();

                    var list = new List<yourTableObj>();

                    foreach (var record in records)
                    {
                        list.Add(MapperEx.CreateFrom<yourTable, yourTableObj>(record));
                    }

                    return list;
                }
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error(ex.FormatException());
                return new List<yourTableObj>();
            }
            catch (Exception ex)
            {
                _log.Error(ex.FormatException());
                return new List<yourTableObj>();
            }
        }

		public async Task<bool> Delete(int id)
        {
            try
            {
                using (var context = _dbContext.NewInstance())
                {
                    var obj = await context.yourTables
                                    .FirstOrDefaultAsync(x => x.Id == id);

                    if(obj != null){
						context.yourTables.Remove(obj);
						context.SaveChanges();
						return true;
					}
					else{
						return false;
						_log.ErrorFormat("failed to delete obj with id '{0}' because record could not be found.",id);
					}
                }
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error(ex.FormatException());
                return false;
            }
            catch (Exception ex)
            {
                _log.Error(ex.FormatException());
                return false;
            }
        }
    }
}

