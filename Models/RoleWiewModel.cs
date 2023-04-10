using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VotoElectronico.Models
{
    public class RoleWiewModel
    {
        public RoleWiewModel()
        {

            ComponentesTree = new List<tbComponentes>();
            ModulosTree = new List<tbModulos>();
            ModuloPantallasTree = new List<tbModuloPantallas>();

            ModuleIdList = new List<int>();
            rol_EsActivo = true;
        }

        [Display(Name = "rol_Id")]
        public int? rol_Id { get; set; }
        [Display(Name = "Nombre Rol")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "La Longitud maxima es de {1}")]
        public string rol_Nombre { get; set; }
        public bool rol_EsActivo { get; set; }

        public IEnumerable<tbComponentes> ComponentesTree { get; set; }
        public IEnumerable<tbModulos> ModulosTree { get; set; }
        public IEnumerable<tbModuloPantallas> ModuloPantallasTree { get; set; }
        

        [HiddenInput]

        public string ModuleItemsInput { get; set; }
        public IEnumerable<int> ModuleIdList { get; set; }

        public void LoadList (IEnumerable<int> modulos)
        {
            ModuleIdList = modulos;
        }
        public void LoadTreeViewData(IEnumerable<tbComponentes> componentes,
          IEnumerable<tbModulos> modulos,
          IEnumerable<tbModuloPantallas> moduloPantallas)

        {
            if (componentes == null && !componentes.Any())
                return;
            if (modulos == null && !modulos.Any())
                return;
            if (moduloPantallas == null && !moduloPantallas.Any())
                return;

            ComponentesTree = componentes;
            ModulosTree = modulos;
            ModuloPantallasTree = moduloPantallas;

            ModuleItemsInput = "";
            if (ModuleIdList.Any())
            {
                foreach (var item in ModuleIdList)
                {
                    ModuleItemsInput += $"{item},";
                        
                }

                ModuleItemsInput = ModuleItemsInput.TrimEnd(',');
            } 

        }

        public void ParseTreeViewData()
        {
            if (!string.IsNullOrWhiteSpace(ModuleItemsInput))
            {
                ModuleIdList = ModuleItemsInput.Split(',')
                    .Where(x => int.TryParse(x, out int mos))
                    .Select(x => int.Parse(x))
                    .ToList();
            }
        }

    }
}