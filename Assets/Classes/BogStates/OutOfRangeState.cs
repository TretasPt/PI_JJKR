using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class OutOfRangeState : State
{
    private Bog bog;

    public OutOfRangeState(Bog bog)
    {
        this.bog = bog;
    }

    public void start()
    {
        
    }

    public void update()
    {
        

        bog.checkRange();
        bog.look();
        bog.move();
        bog.attack();
    }
}
