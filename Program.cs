using System;
using System.Collections.Generic;

class Car
{
    public string Marca { get; private set; }
    public string Model { get; private set; }
    public string Number { get; private set; }
    public string Color { get; private set; }

    private bool isEngineStarted = false;
    private int currentGear = 0;
    private int currentSpeed = 0;

    public Car(string marca, string model, string number, string color)
    {
        Marca = marca;
        Model = model;
        Number = number;
        Color = color;
    }

    public void StartEngine()
    {
        if (!isEngineStarted && currentGear == 0)
        {
            isEngineStarted = true;
        }
    }

    public void StopEngine()
    {
        if (isEngineStarted)
        {
            isEngineStarted = false;
        }
    }

    public void Accelerate(int speed)
    {
        if (isEngineStarted && currentGear > 0 && currentSpeed + speed <= GetMaxSpeedForGear(currentGear))
        {
            currentSpeed += speed;
        }
    }

    public void Brake(int speed)
    {
        if (isEngineStarted && currentSpeed - speed >= 0)
        {
            currentSpeed -= speed;
        }
    }

    public void ChangeGear(int gear)
    {
        if (isEngineStarted && gear >= 0 && gear <= 5 && currentSpeed >= GetMinSpeedForGear(gear) && currentSpeed <= GetMaxSpeedForGear(gear))
        {
            currentGear = gear;
        }
    }

    public string GetStatus()
    {
        if (isEngineStarted)
        {
            return $"Заведена, скорость: {currentSpeed} км/ч, передача: {currentGear}";
        }
        else
        {
            return "Заглушена";
        }
    }

    private int GetMinSpeedForGear(int gear)
    {
        switch (gear)
        {
            case 1: return 0;
            case 2: return 20;
            case 3: return 40;
            case 4: return 60;
            case 5: return 80;
            default: return 0;
        }
    }

    private int GetMaxSpeedForGear(int gear)
    {
        switch (gear)
        {
            case 1: return 30;
            case 2: return 50;
            case 3: return 70;
            case 4: return 90;
            case 5: return 120;
            default: return 0;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Car car1 = new Car("ВАЗ", "2101", "A123BC", "Красный");
        Car car2 = new Car("Mercedes", "600", "X456YZ", "Черный");
        Car car3 = new Car("Toyota", "Camry", "B789DE", "Серебристый");

        List<Car> cars = new List<Car> { car1, car2, car3 };

        Console.WriteLine("Выберите машину (введите номер от 1 до 3):");

        for (int i = 0; i < cars.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {cars[i].Marca} {cars[i].Model} ({cars[i].Number})");
        }

        int selectedCarIndex;
        if (int.TryParse(Console.ReadLine(), out selectedCarIndex) && selectedCarIndex >= 1 && selectedCarIndex <= cars.Count)
        {
            Car selectedCar = cars[selectedCarIndex - 1];
            Console.WriteLine($"Вы выбрали машину: {selectedCar.Marca} {selectedCar.Model} ({selectedCar.Number})");

            while (true)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1. Завести машину");
                Console.WriteLine("2. Заглушить машину");
                Console.WriteLine("3. Газануть");
                Console.WriteLine("4. Притормозить");
                Console.WriteLine("5. Переключить передачу");
                Console.WriteLine("6. Выйти из программы");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            selectedCar.StartEngine();
                            Console.WriteLine($"Машина {selectedCar.Marca} {selectedCar.Model} завелась");
                            break;
                        case 2:
                            selectedCar.StopEngine();
                            Console.WriteLine($"Машина {selectedCar.Marca} {selectedCar.Model} заглохла");
                            break;
                        case 3:
                            Console.WriteLine("Введите скорость для ускорения:");
                            if (int.TryParse(Console.ReadLine(), out int speed))
                            {
                                selectedCar.Accelerate(speed);
                                Console.WriteLine($"Скорость увеличена до {selectedCar.GetStatus()}");
                            }
                            break;
                        case 4:
                            Console.WriteLine("Введите скорость для замедления:");
                            if (int.TryParse(Console.ReadLine(), out int brakeSpeed))
                            {
                                selectedCar.Brake(brakeSpeed);
                                Console.WriteLine($"Скорость уменьшена до {selectedCar.GetStatus()}");
                            }
                            break;
                        case 5:
                            Console.WriteLine("Введите номер передачи (от 0 до 5):");
                            if (int.TryParse(Console.ReadLine(), out int gear))
                            {
                                selectedCar.ChangeGear(gear);
                                Console.WriteLine($"Передача изменена на {selectedCar.GetStatus()}");
                            }
                            break;
                        case 6:
                            Console.WriteLine("Выход из программы.");
                            return;
                        default:
                            Console.WriteLine("Некорректный выбор действия.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный выбор действия.");
                }
            }
        }
        else
        {
            Console.WriteLine("Некорректный выбор машины.");
        }
    }
}