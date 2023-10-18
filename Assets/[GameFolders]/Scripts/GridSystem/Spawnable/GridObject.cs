using System.Collections;
namespace GridSystem.Controllers
{
    public class GridObject : SpawnableBase
    {
        private WarriorController warriorController;
        public WarriorController WarriorController { get { return (warriorController == null) ? warriorController = GetComponent<WarriorController>() : warriorController; } }

        public void GridActive()
        {
            WarriorController.ControllerOn();
        }
        public void GridDeactive()
        {
            WarriorController.ControllerOff();

        }
    }
}

