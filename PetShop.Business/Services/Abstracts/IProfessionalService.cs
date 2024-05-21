using PetShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Business.Services.Abstracts
{
    public interface IProfessionalService
    {
        Task AddAsyncProfessional(Professional professional);
        void DeleteProfessional(int id);
        void UpdateProfessional(int id, Professional newProfessional);
        Professional GetProfessional(Func<Professional, bool>? func = null);
        List<Professional> GetAllProfessionals(Func<Professional, bool>? func = null);
    }
}
