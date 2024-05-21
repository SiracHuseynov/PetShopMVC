using PetShop.Core.Models;
using PetShop.Core.RepositoryAbstracts;
using PetShop.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Data.RepositoryConcretes
{
    public class ProfessionalRepository : GenericRepository<Professional>, IProfessionalRepository
    {
        public ProfessionalRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
