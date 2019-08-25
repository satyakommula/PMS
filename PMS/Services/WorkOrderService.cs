using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMS.Areas.PMS.Models;
using PMS.Areas.PMS.Models.Response;
using PMS.Repository;

namespace PMS.Services
{
    public class WorkOrderService : IWorkOrderService
    {
        private IWorkOrderRepository _workOrderRepository;

        public WorkOrderService(IWorkOrderRepository workOrderRepository)
        {
            _workOrderRepository = workOrderRepository;
        }
        public List<ClientDataModel> getCleints()
        {
            return _workOrderRepository.getCleints();
        }
        public int Insert(tblInwardWO inwardData)
        {
            return _workOrderRepository.Insert(inwardData);
        }

        public int Update(tblInwardWO inwardData)
        {
            return _workOrderRepository.Update(inwardData);
        }

        public int SaveOutward(tblOutwardWO outWardData)
        {
            return _workOrderRepository.SaveOutward(outWardData);
        }

        public int UpdateOutward(tblOutwardWO outWardData)
        {
            return _workOrderRepository.UpdateOutward(outWardData);

        }

        public List<SearchInwardResponseModel> SearchInward(InwardSearchModel searchData)
        {
            return _workOrderRepository.SearchInward(searchData);
        }

        public List<tblInwardWO> GetAll()
        {
            return _workOrderRepository.GetAll();
        }

        public tblInwardWO Get(int id)
        {
            return _workOrderRepository.Get(id);
        }
    }
}