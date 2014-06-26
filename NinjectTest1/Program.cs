using System;
using System.Reflection;
using Ninject;
using Ninject.Modules;

namespace NinjectTest1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            var ninjaWeapon = kernel.Get<IWeapon>("ninja");
            var samuraiWeapon = kernel.Get<IWeapon>("samurai");

            var s = new Samurai(samuraiWeapon);
            var n = new Ninja(ninjaWeapon);
            Console.WriteLine("This Samurai has a {0}, damage={1}", s.Weapon.Name, s.Weapon.Damage);
            Console.WriteLine("This Ninja has a {0}, damage={1}", n.Weapon.Name, n.Weapon.Damage);
        }
    }

    public class WarriorModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWeapon>().To<Sword>().Named("samurai");
            Bind<IWeapon>().To<Shuriken>().Named("ninja");
        }
    }

    public class Ninja
    {
        public Ninja([Named("ninja")] IWeapon weapon)
        {
            Weapon = weapon;
        }

        public IWeapon Weapon { get; set; }
    }

    public class Samurai
    {
        public Samurai([Named("samurai")] IWeapon weapon)
        {
            Weapon = weapon;
        }

        public IWeapon Weapon { get; private set; }
    }



    public class GunAttribute : Attribute
    {}

    public class SwordAttribute : Attribute
    {}

    public class Sword : IWeapon
    {
        public string Name
        {
            get { return "Sword"; }
        }

        public int Damage { get { return 150; }}
    }
    public class Katana : IWeapon
    {
        public string Name
        {
            get { return "Katana"; }
        }

        public int Damage { get { return 200; } }
    }

    public class Shuriken : IWeapon
    {
        public string Name
        {
            [Gun]
            get { return "Shuriken"; }
        }

        public int Damage { get { return 10; } }
    }

    public interface IWeapon
    {
        string Name { get;}
        int Damage { get;}
    }
}