﻿/**
 * Author: Pantelis Andrianakis
 * Date: December 28th 2017
 */
class CharacterDataHolder
{
    string name = "";
    byte slot = 0;
    bool selected = false;
    byte classId = 0;
    string locationName = "";
    float x = 0;
    float y = 0;
    float z = 0;
    float heading = 0;
    long experience = 0;
    long hp = 0;
    long mp = 0;
    byte accessLevel = 0;

    public string GetName()
    {
        return name;
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public byte GetSlot()
    {
        return slot;
    }

    public void SetSlot(byte slot)
    {
        this.slot = slot;
    }

    public bool IsSelected()
    {
        return selected;
    }

    public void SetSelected(bool selected)
    {
        this.selected = selected;
    }

    public byte GetClassId()
    {
        return classId;
    }

    public void SetClassId(byte classId)
    {
        this.classId = classId;
    }

    public string GetLocationName()
    {
        return locationName;
    }

    public void SetLocationName(string locationName)
    {
        this.locationName = locationName;
    }

    public float GetX()
    {
        return x;
    }

    public void SetX(float x)
    {
        this.x = x;
    }

    public float GetY()
    {
        return y;
    }

    public void SetY(float y)
    {
        this.y = y;
    }

    public float GetZ()
    {
        return z;
    }

    public void SetZ(float z)
    {
        this.z = z;
    }

    public float GetHeading()
    {
        return heading;
    }

    public void SetHeading(float heading)
    {
        this.heading = heading;
    }

    public long GetExperience()
    {
        return experience;
    }

    public void SetExperience(long experience)
    {
        this.experience = experience;
    }

    public long GetHp()
    {
        return hp;
    }

    public void SetHp(long hp)
    {
        this.hp = hp;
    }

    public long GetMp()
    {
        return mp;
    }

    public void SetMp(long mp)
    {
        this.mp = mp;
    }

    public byte GetAccessLevel()
    {
        return accessLevel;
    }

    public void SetAccessLevel(byte accessLevel)
    {
        this.accessLevel = accessLevel;
    }
}
