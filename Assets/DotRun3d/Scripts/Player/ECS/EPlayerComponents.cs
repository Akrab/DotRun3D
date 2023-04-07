using DotRun3d.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonRun3D.ECS.Player
{
    public struct EPlayerComp
    {
        public IPlayerView playerView { get; set; }
    }

    public struct EPlayerCreate
    {

    }
}


