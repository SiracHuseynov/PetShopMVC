using Microsoft.AspNetCore.Hosting;
using PetShop.Business.Exceptions;
using PetShop.Business.Extensions;
using PetShop.Business.Services.Abstracts;
using PetShop.Core.Models;
using PetShop.Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Business.Services.Concretes
{
    public class ProfessionalService : IProfessionalService
    {
        private readonly IProfessionalRepository _professionalRepository;
        private readonly IWebHostEnvironment _env;

        public ProfessionalService(IProfessionalRepository professionalRepository, IWebHostEnvironment env)
        {
            _professionalRepository = professionalRepository;
            _env = env;
        }

        public async Task AddAsyncProfessional(Professional professional)
        {
            if (professional.ImageFile == null)
                throw new ImageNotFoundException("Image olmalidir!");
            
            professional.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\professionals", professional.ImageFile);   

            await _professionalRepository.AddAsync(professional);
            await _professionalRepository.CommitAsync();
        }

        public void DeleteProfessional(int id)
        {
            var existProfessional = _professionalRepository.Get(x => x.Id == id);

            if (existProfessional == null)
                throw new EntityNotFoundException("Professional tapilmadi!"); 

            Helper.DeleteFile(_env.WebRootPath, @"uploads\professionals", existProfessional.ImageUrl);

            _professionalRepository.Delete(existProfessional);
            _professionalRepository.Commit();
        }

        public List<Professional> GetAllProfessionals(Func<Professional, bool>? func = null)
        {
            return _professionalRepository.GetAll(func);
        }

        public Professional GetProfessional(Func<Professional, bool>? func = null)
        {
            return _professionalRepository.Get(func);
        }

        public void UpdateProfessional(int id, Professional newProfessional)
        {
            var oldProfessional = _professionalRepository.Get(x => x.Id == id);

            if (oldProfessional == null)
                throw new EntityNotFoundException("Professional tapilmadi!");

            if(newProfessional.ImageFile != null)
            {
                Helper.DeleteFile(_env.WebRootPath, @"uploads\professionals", oldProfessional.ImageUrl);

                oldProfessional.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\professionals", newProfessional.ImageFile);

            }

            oldProfessional.FullName = newProfessional.FullName;
            oldProfessional.Profession = newProfessional.Profession;

            _professionalRepository.Commit();
        }
    }
}
