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
/// This file is generated from NetGenerator
/// providing basic features of business domain object
/// </summary>
namespace YourProject.BusinessLogic.Services
{
    public partial class TableSample2ObjService
    {
        private IYourProject _dbContext;
        private ILog _log = LogManager.GetLogger(typeof(TableSample2ObjService));

        public TableSample2ObjService()
        {
            _dbContext = new YourProjectContext();
        }

        public async Task<TableSample2Obj> GetById(int id)
        {
            try
            {
                using (var context = _dbContext.NewInstance())
                {
                    var obj = await context.TableSample2s
                                    .FirstOrDefaultAsync(x => x.Id == id);

                    var detail = new TableSample2Obj();
                    detail = MapperEx.CreateFrom<TableSample2, TableSample2Obj>(obj);
                    return detail;
                }
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error(ex.FormatException());
                return new TableSample2Obj();
            }
            catch (Exception ex)
            {
                _log.Error(ex.FormatException());
                return new TableSample2Obj();
            }
        }

        public async Task<bool> AddOrUpdate(TableSample2Obj obj)
        {
            try
            {
                using (var context = _dbContext.NewInstance())
                {
                    var tryFind = await context.TableSample2Obj
                                            .FirstOrDefaultAsync(x => x.Id == obj.Id);

                    var updateingRecord = new TableSample2();
                    if (tryFind != null)
                    {
                        MapperEx.Map(obj, ref updatingRecord);
                    }
                    else
                    {
                        updatingRecord = MapperEx.CreateFrom<TableSample2Obj, TableSample2>(obj);
                    }

                    context.TableSample2s.AddOrUpdate(updatingRecord);
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

        
        public IList<TableSample2Obj> Search(out int totalCount, int start = 0, int pageSize = 10, string sortField = "", string sortDirection = "desc", string search = "")
        {
            totalCount = 0;

            try
            {
                using (var context = _dbContext.NewInstance())
                {
                    var objs = context.TableSample2s;

                    var records = objs.OrderByEx(sortDirection, sortField)
                                      .Skip(start).Take(pageSize).ToList();

                    totalCount = context.TableSample2s.Count();

                    var list = new List<TableSample2Obj>();

                    foreach (var record in records)
                    {
                        list.Add(MapperEx.CreateFrom<TableSample2, TableSample2Obj>(record));
                    }

                    return list;
                }
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error(ex.FormatException());
                return new List<TableSample2Obj>();
            }
            catch (Exception ex)
            {
                _log.Error(ex.FormatException());
                return new List<TableSample2Obj>();
            }
        }

		public async Task<bool> Delete(int id)
        {
            try
            {
                using (var context = _dbContext.NewInstance())
                {
                    var obj = await context.TableSample2s
                                    .FirstOrDefaultAsync(x => x.Id == id);

                    if(obj != null){
						context.TableSample2s.Remove(obj);
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

    public partial class TableSampleObjService
    {
        private IYourProject _dbContext;
        private ILog _log = LogManager.GetLogger(typeof(TableSampleObjService));

        public TableSampleObjService()
        {
            _dbContext = new YourProjectContext();
        }

        public async Task<TableSampleObj> GetById(int id)
        {
            try
            {
                using (var context = _dbContext.NewInstance())
                {
                    var obj = await context.TableSamples
                                    .FirstOrDefaultAsync(x => x.Id == id);

                    var detail = new TableSampleObj();
                    detail = MapperEx.CreateFrom<TableSample, TableSampleObj>(obj);
                    return detail;
                }
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error(ex.FormatException());
                return new TableSampleObj();
            }
            catch (Exception ex)
            {
                _log.Error(ex.FormatException());
                return new TableSampleObj();
            }
        }

        public async Task<bool> AddOrUpdate(TableSampleObj obj)
        {
            try
            {
                using (var context = _dbContext.NewInstance())
                {
                    var tryFind = await context.TableSampleObj
                                            .FirstOrDefaultAsync(x => x.Id == obj.Id);

                    var updateingRecord = new TableSample();
                    if (tryFind != null)
                    {
                        MapperEx.Map(obj, ref updatingRecord);
                    }
                    else
                    {
                        updatingRecord = MapperEx.CreateFrom<TableSampleObj, TableSample>(obj);
                    }

                    context.TableSamples.AddOrUpdate(updatingRecord);
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

        
        public IList<TableSampleObj> Search(out int totalCount, int start = 0, int pageSize = 10, string sortField = "", string sortDirection = "desc", string search = "")
        {
            totalCount = 0;

            try
            {
                using (var context = _dbContext.NewInstance())
                {
                    var objs = context.TableSamples;

                    var records = objs.OrderByEx(sortDirection, sortField)
                                      .Skip(start).Take(pageSize).ToList();

                    totalCount = context.TableSamples.Count();

                    var list = new List<TableSampleObj>();

                    foreach (var record in records)
                    {
                        list.Add(MapperEx.CreateFrom<TableSample, TableSampleObj>(record));
                    }

                    return list;
                }
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error(ex.FormatException());
                return new List<TableSampleObj>();
            }
            catch (Exception ex)
            {
                _log.Error(ex.FormatException());
                return new List<TableSampleObj>();
            }
        }

		public async Task<bool> Delete(int id)
        {
            try
            {
                using (var context = _dbContext.NewInstance())
                {
                    var obj = await context.TableSamples
                                    .FirstOrDefaultAsync(x => x.Id == id);

                    if(obj != null){
						context.TableSamples.Remove(obj);
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