﻿/**
 * Author: Pantelis Andrianakis
 * Date: November 7th 2018
 */
class Creature : WorldObject
{
    public override bool IsCreature()
    {
        return true;
    }

    public override Creature AsCreature()
    {
        return this;
    }
}
