using System.Collections.Generic;
using TheTop.Application.Services.DTOs;

namespace TheTop.Application.Services
{
    public interface IContractService
    {
        void CreateNewContract(ContractDTO contractDto);
        void UpdateContract(ContractDTO contractDto);
        IEnumerable<ContractDTO> GetAllContract();
    }
}