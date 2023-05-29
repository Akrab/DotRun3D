using DonRun3D.LevelConstructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonRun3D.ECS.LevelContructor
{

    public struct ELevelConstrComp
    {
        public ILevelConstructorView view;
    }

    public struct ECreateLevelComp { }
    public struct EHideLevelComp { }
    public struct EUpdateRuntimeLevelComp { }
}
