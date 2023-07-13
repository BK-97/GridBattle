using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GridSystem.Controllers
{
    public class GridObject : SpawnableBase
    {

        private WarriorController warriorController;
        private void Start()
        {
            Initialize();
        }
        public override void Initialize()
        {
            warriorController = GetComponent<WarriorController>();

        }
        public void GridActive()
        {
            warriorController.ControllerOn();
        }
        public void GridDeactive()
        {
            warriorController.ControllerOff();

        }
    }
}

