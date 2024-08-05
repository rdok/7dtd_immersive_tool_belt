using System;

namespace ImmersiveToolBelt.Harmony.Interfaces
{
    public interface IDateTime
    {
        DateTime Now();
        double TotalSeconds();
    }
}