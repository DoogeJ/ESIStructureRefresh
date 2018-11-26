using System;
using System.Collections.Generic;

public class StructuresRequestResult
{
    public int corporation_id;
    public string fuel_expires;
    public int profile_id;
    public int reinforce_hour;
    public int reinforce_weekday;
    public List<StructuresServicesRequestResult> services;
    public string state;
    public Int64 structure_id;
    public int system_id;
    public int type_id;
}

public class StructuresServicesRequestResult
{
    public string name;
    public string state;
}
