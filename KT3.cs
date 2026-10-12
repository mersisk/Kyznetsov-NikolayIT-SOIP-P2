 using System;
using System.Collections.Generic;

class Movie
{
     public string Name { get; set; }
    public int Duration { get; set; }


public Movie(string name, int duration)
    {
        Name = name;
        Duration = duration;
    }
     public override string ToString()
    {
        return Name + " - " + Duration + " мин.";
    }
}


class Seat
{
    public int Number { get; set; }
    public bool IsReserved { get; private set; }

    public Seat(int number)
    {
        Number = number;
        IsReserved = false;
    }

    public void Reserve()
    {
        if (IsReserved)
        {
            throw new Exception("Место уже занято.");
        }

        IsReserved = true;
    }

    public void CancelReservation()
    {
        IsReserved = false;
    }
}
class CinemaHall
{
    public List<Seat> Seats { get; set; }

    public CinemaHall(int seatCount)
    {
        Seats = new List<Seat>();

        for (int i = 1; i <= seatCount; i++)
        {
            Seats.Add(new Seat(i));
        }
    }

    public void ShowSeats()
    {
        Console.WriteLine("\nСхема зала:");

        foreach (Seat seat in Seats)
        {
            if (seat.IsReserved)
            {
                Console.Write("[X] ");
            }
            else
            {
                Console.Write("[" + seat.Number + "] ");
            }
        }

        Console.WriteLine();
        Console.WriteLine("X - место занято");
    }

    
    
    public Seat GetSeat(int number)
    {
        foreach (Seat seat in Seats)
        {
            if (seat.Number == number)
            {
                return seat;
            }
        }

        return null;
    }
}

class Booking
{
    public Movie Movie { get; set; }
    public Seat Seat { get; set; }

    public Booking(Movie movie, Seat seat)
    {
        Movie = movie;
        Seat = seat;
    }

    public override string ToString()
    {
        return Movie.Name + ", место №" + Seat.Number;
    }
}

class Customer
{
    public string Name { get; set; }
    public List<Booking> Bookings { get; set; }

    public Customer(string name)
    {
        Name = name;
        Bookings = new List<Booking>();
    }

    public void ShowBookings()
    {
        Console.WriteLine("\nБронирования клиента " + Name + ":");

        if (Bookings.Count == 0)
        {
            Console.WriteLine("Бронирований нет.");
            return;
        }

        for (int i = 0; i < Bookings.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + Bookings[i]);
        }
    }
}

class BookingService
{
    public Movie SelectMovie(List<Movie> movies, int number)
    {
        if (number < 1 || number > movies.Count)
        {
            return null;
        }

        return movies[number - 1];
    }

    public Seat SelectSeat(CinemaHall hall, int number)
    {
        return hall.GetSeat(number);
    }

    public void MakeBooking(Movie movie, Seat seat, Customer customer)
    {
        if (movie == null)
        {
            Console.WriteLine("Фильм не найден.");
            return;
        }

        if (seat == null)
        {
            Console.WriteLine("Такого места нет.");
            return;
        }

        try
        {
            seat.Reserve();

            Booking booking = new Booking(movie, seat);
            customer.Bookings.Add(booking);

            Console.WriteLine("\nБронирование успешно!");
            Console.WriteLine("Фильм: " + movie.Name);
            Console.WriteLine("Место: " + seat.Number);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void CancelBooking(Customer customer, int bookingNumber)
    {
        if (bookingNumber < 1 || bookingNumber > customer.Bookings.Count)
        {
            Console.WriteLine("Такого бронирования нет.");
            return;
        }

        Booking booking = customer.Bookings[bookingNumber - 1];

        booking.Seat.CancelReservation();
        customer.Bookings.Remove(booking);

        Console.WriteLine("Бронирование отменено.");
    }
}

class Program
{
    static void Main()
    {
        List<Movie> movies = new List<Movie>();

        movies.Add(new Movie("Мстители", 143));
        movies.Add(new Movie("Человек-паук", 148));
        movies.Add(new Movie("Интерстеллар", 169));

        CinemaHall hall = new CinemaHall(10);
        BookingService service = new BookingService();

        Console.Write("Введите ваше имя: ");
        string name = Console.ReadLine();

        Customer customer = new Customer(name);

        Console.WriteLine("\nСписок фильмов:");

        for (int i = 0; i < movies.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + movies[i]);
        }

        Console.Write("\nВыберите фильм: ");
        int movieNumber;

        if (!int.TryParse(Console.ReadLine(), out movieNumber))
        {
            Console.WriteLine("Нужно ввести число.");
            return;
        }

        Movie selectedMovie = service.SelectMovie(movies, movieNumber);

        if (selectedMovie == null)
        {
            Console.WriteLine("Такого фильма нет.");
            return;
        }

        Console.WriteLine("\nВы выбрали фильм: " + selectedMovie.Name);

        hall.ShowSeats();

        Console.Write("\nВыберите место от 1 до 10: ");
        int seatNumber;

        if (!int.TryParse(Console.ReadLine(), out seatNumber))
        {
            Console.WriteLine("Нужно ввести число.");
            return;
        }

        Seat selectedSeat = service.SelectSeat(hall, seatNumber);

        service.MakeBooking(selectedMovie, selectedSeat, customer);

        hall.ShowSeats();

        customer.ShowBookings();

        Console.Write("\nХотите отменить бронирование? (да/нет): ");
        string answer = Console.ReadLine();

        if (answer.ToLower() == "да")
        {
            customer.ShowBookings();

            if (customer.Bookings.Count > 0)
            {
                Console.Write("Введите номер бронирования для отмены: ");

                int bookingNumber;

                if (int.TryParse(Console.ReadLine(), out bookingNumber))
                {
                    service.CancelBooking(customer, bookingNumber);
                }
                else
                {
                    Console.WriteLine("Нужно ввести число.");
                }
            }
        }

        Console.WriteLine("\nИтоговая схема зала:");
        hall.ShowSeats();

        customer.ShowBookings(); 
    }
}
