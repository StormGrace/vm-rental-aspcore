using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vm_rental
{
    public class EntityTemplate : CSharpEntityTypeGenerator
    {
        public EntityTemplate(ICSharpHelper cSharpUtilities) : base(cSharpUtilities) { }

        public override string WriteCode(IEntityType entityType, string @namespace, bool useDataAnnotations)
        {
            string code = base.WriteCode(entityType, @namespace, useDataAnnotations);

            var oldString = "public partial class " + entityType.Name;
            var newString = "public partial class " + entityType.Name + " : EntityBase";

            return code.Replace(oldString, newString);
        }
    }
}
