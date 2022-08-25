using Entidades.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Column("USER_CPF")]
        public string? CPF { get; set; }
        [Column("USER_TIPO")]
        public TipoUsuario? Tipo { get; set; }
    }
}
