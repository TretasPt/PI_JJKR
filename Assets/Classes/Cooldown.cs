using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Cooldown
{
    float time;
    DateTime start;

    public Cooldown(float cooldownTime)
    {
        time = cooldownTime;
        start = new DateTime();
    }

    public void setStart()
    {
        start = DateTime.Now;
    }

    public bool done()
    {
        return (DateTime.Now - start).TotalSeconds > time;
    }
};
