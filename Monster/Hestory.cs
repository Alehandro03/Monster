using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Monster
{
    class Hestory
    {
        public static int chek = 1;

        public static bool startMsg = true;
        
        public static Personage wolf = new Personage("Волк", MonsterType.Animal,1500,100,100,5,10,
            "РРРРР!!!!!","...И...и...",5);
        public static Personage wolf2 = new Personage("Волк", MonsterType.Animal, 1500, 100, 100, 5, 10,
            "РРРРР!!!!!", "...И...и...", 5);

        public static Personage yrykhi = new Personage("Урукхай", MonsterType.Orc,3500,110,110,9,13,  
            "Тебе конец маленький {Имя персонажа}","РРРыАрЫАрАВрА!",10);

        public static Personage smayg = new Personage("Смауг", MonsterType.Dracon, 5000, 200, 200, 10, 20, 
            "Попалась маленькая букашка!!!!", "РАААА!!!Нет!!!", 20);


        public static void StartText(ITelegramBotClient botClient, Update update, CancellationToken token, ref bool a, Personage pers)
        {
            var message = update.Message; // Эта переменная будет содержать в себе все связанное с сообщениями

            var user = message.From; //- это от кого пришло сообщение (или любой другой Update)

            Console.WriteLine($"{user.FirstName} ({user.Id}) написал сообщение: {message.Text}");

            var chat = message.Chat; //- содержит всю информацию о чате

            Thread.Sleep(1000);

            botClient.SendTextMessageAsync(chat.Id, "Приветствую Вас на данной тропе.\nВас ждет небольшое приключение.\n" +
                "Вы должны показать максимум сноровки и везения!\nПосле чего вы встретитесь и сразитесь между собой!\nУдачи!");
           
        }

        public static void RoadOne(ITelegramBotClient botClient, Update update, CancellationToken token, ref bool RO, Personage pers, ref bool shop, ref bool RoadOne)
        {
            Thread.Sleep(1000);
           
            switch (update.Type) // Сразу же ставим конструкцию switch, чтобы обрабатывать приходящие Update
            {
                case UpdateType.CallbackQuery:
                    {

                        var callbackQuery = update.CallbackQuery;// Переменная,которая будет содержать в себе всю информацию о кнопке,
                                                                 // которую нажали.                         
                        var user = callbackQuery.From;// Аналогично и с Message мы можем получить информацию о чате, о пользователе и т.д.

                        // Выводим на экран нажатие кнопки
                        Console.WriteLine($"{user.FirstName} ({user.Id}) нажал на кнопку: {callbackQuery.Data}");

                        // Вот тут нужно уже быть немножко внимательным и не путаться!
                        // Мы пишем не callbackQuery.Chat , а callbackQuery.Message.Chat , так как
                        // кнопка привязана к сообщению, то мы берем информацию от сообщения.
                        var chat = callbackQuery.Message.Chat;

                        // Добавляем блок switch для проверки кнопок
                        switch (callbackQuery.Data)
                        {
                            // Data - это придуманный нами id кнопки, мы его указывали в параметре
                            // callbackData при создании кнопок. У меня это button1, button2 и button3

                            case "button4":
                                {
                                    
                                    botClient.SendTextMessageAsync(chat.Id, $"Удар!!");

                                    wolf.Hp = wolf.Hp - pers.GetAttack(pers.MinAttack, pers.MaxAttack,pers.GetUseItemAtPers(pers));

                                    pers.Hp = pers.Hp  - wolf.MinAttack;

                                    if (wolf.Hp <= 0)
                                    {

                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), $"{wolf.DeadCry}", showAlert: true);
                                        botClient.SendTextMessageAsync(chat.Id,"Волк убит!" );
                                        Thread.Sleep(1000);//замедляем время вывода.

                                        botClient.SendTextMessageAsync(chat.Id, "Волк делает последние издыханиея и падает замертво!\n" +
                                            "При обыске вы находите награду!");
                                        botClient.AnswerCallbackQueryAsync(callbackQuery.Id, $"Золото:{wolf.Cash}", showAlert: true);
                                       
                                        pers.Cash += wolf.Cash;

                                        chek = 1;
                                        shop = true;
                                        RoadOne = false;

                                        var inlineKeyboard = new InlineKeyboardMarkup(
                                           new List<InlineKeyboardButton[]>() // здесь создаем лист (массив),который содрежит в себе массив из класса кнопок 
                                           {               // Каждый новый массив - это дополнительные строки,а каждая дополнительная кнопка в массиве - это добавление ряда                    

                                                   new InlineKeyboardButton[] // тут создаем массив кнопок
                                                   {
                                                      InlineKeyboardButton.WithCallbackData("В магазин", "button1"),
                                                      InlineKeyboardButton.WithCallbackData("В бой", "button2"),
                                                      InlineKeyboardButton.WithCallbackData("В инвентарь", "button3"),
                                                   },

                                           });

                                        Thread.Sleep(1000);//замедляем время вывода.
                                        botClient.SendTextMessageAsync(chat.Id, $"Можно и передохнуть!:"
                                          , replyMarkup: inlineKeyboard);//Все клавиатуры передаются в параметр replyMarkup

                                        return;

                                    }
                                    else if(pers.Hp <= 0)
                                    {
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), $"{pers.IsDie}", showAlert: true);
                                        botClient.SendTextMessageAsync(chat.Id, "Вы убиты!");
                                        return;
                                    }
                                    else
                                    {
                                        Thread.Sleep(1000);//замедляем время вывода.
                                        botClient.SendTextMessageAsync(chat.Id, $"Ник: {pers.Name}\nЗдоровье: {pers.Hp}\n" +
                                                $"Урон: {pers.MinAttack}\nБроня: {pers.Armor}\nЗолото: {pers.Cash}");
                                        Thread.Sleep(1000);//замедляем время вывода.
                                        botClient.SendTextMessageAsync(chat.Id, $"Волк:\nЗдоровье: {wolf.Hp}\n" +
                                                $"Урон: {wolf.MinAttack}\nБроня: {wolf.Armor}");
                                        var inlineKeyboard = new InlineKeyboardMarkup(
                                           new List<InlineKeyboardButton[]>() // здесь создаем лист (массив),который содрежит в себе массив из класса кнопок 
                                           {               // Каждый новый массив - это дополнительные строки,а каждая дополнительная кнопка в массиве - это добавление ряда                    

                                                   new InlineKeyboardButton[] // тут создаем массив кнопок
                                                   {
                                                       InlineKeyboardButton.WithCallbackData("Удар!", "button4"),
                                                       InlineKeyboardButton.WithCallbackData("Инвентарь...", "button5"),
                                                   },

                                           });
                                        Thread.Sleep(1000);//замедляем время вывода.
                                        

                                        chek++;
                                        botClient.SendTextMessageAsync(chat.Id, $"{chek}РАУНД!"
                                          , replyMarkup: inlineKeyboard);
                                        return;
                                    }


                                    
                                    
                                }

                            case "button5":
                                {
                                    botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "Идем в инвентарь!", showAlert: true);

                                    botClient.SendTextMessageAsync(chat.Id, $"{pers.Name} идет в инвентарь!");

                                    if (pers.item.Count != 0)
                                    {
                                        Thread.Sleep(1000);//замедляем время вывода.
                                        botClient.SendTextMessageAsync(chat.Id, $"Ник: {pers.Name}\nЗдоровье: {pers.Hp}\n" +
                                                $"Урон: {pers.MinAttack}\nБроня: {pers.Armor}\nЗолото: {pers.Cash}");
                                        for (int i = 0; i < pers.item.Count; i++)
                                        {
                                            string tmp = " ";
                                            if (pers.item[i].Use == true) tmp = "Используется!";
                                            else if (pers.item[i].Use == false) tmp = "Не используется!";
                                            botClient.SendTextMessageAsync(chat.Id, $"{pers.item[i]._typeOfItems}Цена:{pers.item[i].Cost}\nУрон:{pers.item[i]._damage}\n" +
                                                $"Защита:{pers.item[i]._protection}\nИспользование:{tmp}\nИнформация:{pers.item[i]._info}");
                                            if (pers.item[i].ItemText == "Склянка здоровья")
                                            {
                                                botClient.SendTextMessageAsync(chat.Id, $"{pers.item[i]._typeOfItems}Цена:{pers.item[i].Cost}\nУрон:{pers.item[i]._damage}\n" +
                                                $"Защита:{pers.item[i]._protection}\nИспользование:{tmp}\nИнформация:{pers.item[i]._info}\n" +
                                                $"Применить жми:/apply{pers.item[i]._typeOfItems}");
                                            }
                                        }
                                    }

                                    else botClient.SendTextMessageAsync(chat.Id, "Инвентарь пуст!");

                                    return;
                                }

                        }

                        return;
                    }
                case UpdateType.Message:
                    {
                        var message = update.Message; // Эта переменная будет содержать в себе все связанное с сообщениями

                        var user = message.From; //- это от кого пришло сообщение (или любой другой Update)

                        Console.WriteLine($"{user.FirstName} ({user.Id}) написал сообщение: {message.Text}");

                        var chat = message.Chat; //- содержит всю информацию о чате

                        var name = message.Text;                        

                        switch (message.Type) // Добавляем проверку на тип Message
                        {

                            case MessageType.Text:  // Тут понятно, текстовый тип
                                {
                                    if(message.Text == "/Attak")
                                    {
                                        // В этом типе клавиатуры обязательно нужно использовать следующий метод
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "В бой!!", showAlert: true);
                                        // Для того, чтобы отправить телеграмму запрос, что мы нажали на кнопку

                                        botClient.SendTextMessageAsync(chat.Id, $"{pers.Name} не думая бросается на волка из кустов!!!");

                                        Thread.Sleep(1000);
                                        botClient.SendTextMessageAsync(chat.Id, $"1 РАУНД!");

                                        var inlineKeyboard = new InlineKeyboardMarkup(
                                           new List<InlineKeyboardButton[]>() // здесь создаем лист (массив),который содрежит в себе массив из класса кнопок 
                                           {               // Каждый новый массив - это дополнительные строки,а каждая дополнительная кнопка в массиве - это добавление ряда                    

                                                   new InlineKeyboardButton[] // тут создаем массив кнопок
                                                   {
                                                       InlineKeyboardButton.WithCallbackData("Удар!", "button4"),
                                                       InlineKeyboardButton.WithCallbackData("Инвентарь...", "button5"),
                                                   },

                                           });

                                        Thread.Sleep(1000);//замедляем время вывода.
                                        botClient.SendTextMessageAsync(chat.Id, $"Выбор за тобой!!!:"
                                          , replyMarkup: inlineKeyboard);//Все клавиатуры передаются в параметр replyMarkup

                                        return;
                                    }
                                    else if(message.Text == "/Escape")
                                    {
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Сбежать!", showAlert: true);

                                        bool esc = MetodsForPlay.EscapeLuck();

                                        if (esc == true)
                                        {
                                            Thread.Sleep(3000);
                                            botClient.SendTextMessageAsync(chat.Id, $"{pers.Name} тебе удалось умыкнуть от волка\n" +
                                                $"Но и золота ты не получил.)");

                                            shop = true;
                                            RoadOne = false;

                                            var inlineKeyboard = new InlineKeyboardMarkup(
                                               new List<InlineKeyboardButton[]>() // здесь создаем лист (массив),который содрежит в себе массив из класса кнопок 
                                               {               // Каждый новый массив - это дополнительные строки,а каждая дополнительная кнопка в массиве - это добавление ряда                    

                                                   new InlineKeyboardButton[] // тут создаем массив кнопок
                                                   {
                                                      InlineKeyboardButton.WithCallbackData("В магазин", "button1"),
                                                      InlineKeyboardButton.WithCallbackData("В бой", "button2"),
                                                      InlineKeyboardButton.WithCallbackData("В инвентарь", "button3"),
                                                   },

                                               });

                                            Thread.Sleep(1000);//замедляем время вывода.
                                            botClient.SendTextMessageAsync(chat.Id, $"Можно и передохнуть!:"
                                              , replyMarkup: inlineKeyboard);//Все клавиатуры передаются в параметр replyMarkup

                                            return;
                                        }
                                        else
                                        {
                                            Thread.Sleep(3000);
                                            botClient.SendTextMessageAsync(chat.Id, $"Волк:{wolf.WarCry}\n{pers.Name} тебе не удалось умыкнуть от волка\n" +
                                                $"Волк заметил тебя. Готовся!!!!)");

                                            Thread.Sleep(1000);
                                            botClient.SendTextMessageAsync(chat.Id, $"1 РАУНД!");

                                           var inlineKeyboard = new InlineKeyboardMarkup(
                                           new List<InlineKeyboardButton[]>() // здесь создаем лист (массив),который содрежит в себе массив из класса кнопок 
                                           {               // Каждый новый массив - это дополнительные строки,а каждая дополнительная кнопка в массиве - это добавление ряда                    

                                                   new InlineKeyboardButton[] // тут создаем массив кнопок
                                                   {
                                                       InlineKeyboardButton.WithCallbackData("Удар!", "button4"),
                                                       InlineKeyboardButton.WithCallbackData("Инвентарь...", "button5"),
                                                   },

                                           });

                                            Thread.Sleep(1000);//замедляем время вывода.
                                            botClient.SendTextMessageAsync(chat.Id, $"Волк в прыжке набрасывается на Вас!"
                                              , replyMarkup: inlineKeyboard);//Все клавиатуры передаются в параметр replyMarkup
                                        }

                                        return;
                                    }

                                    if (message.Text == "/applyBottleOfHealth")
                                    {
                                        pers.Hp = 100;
                                        Item bottle = RealItems.CreateBottle();
                                        botClient.SendTextMessageAsync(chat.Id, Inventar.DeleteItem(pers, bottle));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Склянка использована!", showAlert: true);

                                    }


                                    return;

                                }

                            default: // Добавил default , чтобы показать вам разницу типов Message
                                {
                                    botClient.SendTextMessageAsync(chat.Id, "Используй только кнопки!");
                                    return;
                                }
                        }
                       

                    }




            }

          
        }

        public static void RoadTwo(ITelegramBotClient botClient, Update update, CancellationToken token, ref bool RO, Personage pers, ref bool shop, ref bool RoadTwo)
        {
            Thread.Sleep(1000);

            switch (update.Type) // Сразу же ставим конструкцию switch, чтобы обрабатывать приходящие Update
            {
                case UpdateType.CallbackQuery:
                    {

                        var callbackQuery = update.CallbackQuery;// Переменная,которая будет содержать в себе всю информацию о кнопке,
                                                                 // которую нажали.                         
                        var user = callbackQuery.From;// Аналогично и с Message мы можем получить информацию о чате, о пользователе и т.д.

                        // Выводим на экран нажатие кнопки
                        Console.WriteLine($"{user.FirstName} ({user.Id}) нажал на кнопку: {callbackQuery.Data}");

                        // Вот тут нужно уже быть немножко внимательным и не путаться!
                        // Мы пишем не callbackQuery.Chat , а callbackQuery.Message.Chat , так как
                        // кнопка привязана к сообщению, то мы берем информацию от сообщения.
                        var chat = callbackQuery.Message.Chat;

                        // Добавляем блок switch для проверки кнопок
                        switch (callbackQuery.Data)
                        {
                            // Data - это придуманный нами id кнопки, мы его указывали в параметре
                            // callbackData при создании кнопок. У меня это button1, button2 и button3

                            case "button6":
                                {

                                    botClient.SendTextMessageAsync(chat.Id, $"Удар!!");

                                    yrykhi.Hp = yrykhi.Hp - pers.GetAttack(pers.MinAttack, pers.MaxAttack, pers.GetUseItemAtPers(pers));

                                    pers.Hp = pers.Hp - yrykhi.MinAttack;

                                    if (yrykhi.Hp <= 0)
                                    {

                                        botClient.AnswerCallbackQueryAsync(callbackQuery.Id, $"{yrykhi.DeadCry}", showAlert: true);
                                        botClient.SendTextMessageAsync(chat.Id, "Орк убит!");
                                        Thread.Sleep(1000);//замедляем время вывода.

                                        botClient.SendTextMessageAsync(chat.Id, "У орка слетае голова с плеч!\n" +
                                            "При обыске вы находите награду!");
                                        botClient.AnswerCallbackQueryAsync(callbackQuery.Id, $"Золото:{yrykhi.Cash}", showAlert: true);

                                        pers.Cash += yrykhi.Cash;

                                        chek = 1;
                                        shop = true;
                                        RoadTwo = false;

                                        var inlineKeyboard = new InlineKeyboardMarkup(
                                           new List<InlineKeyboardButton[]>() // здесь создаем лист (массив),который содрежит в себе массив из класса кнопок 
                                           {               // Каждый новый массив - это дополнительные строки,а каждая дополнительная кнопка в массиве - это добавление ряда                    

                                                   new InlineKeyboardButton[] // тут создаем массив кнопок
                                                   {
                                                      InlineKeyboardButton.WithCallbackData("В магазин", "button1"),
                                                      InlineKeyboardButton.WithCallbackData("В бой", "button2"),
                                                      InlineKeyboardButton.WithCallbackData("В инвентарь", "button3"),
                                                   },

                                           });

                                        Thread.Sleep(1000);//замедляем время вывода.
                                        botClient.SendTextMessageAsync(chat.Id, $"Можно и передохнуть!:"
                                          , replyMarkup: inlineKeyboard);//Все клавиатуры передаются в параметр replyMarkup

                                        return;

                                    }
                                    else if (pers.Hp <= 0)
                                    {
                                        botClient.AnswerCallbackQueryAsync(callbackQuery.Id, $"{pers.IsDie}", showAlert: true);
                                        botClient.SendTextMessageAsync(chat.Id, "Вы убиты!");
                                        return;
                                    }
                                    else
                                    {
                                        Thread.Sleep(1000);//замедляем время вывода.
                                        botClient.SendTextMessageAsync(chat.Id, $"Ник: {pers.Name}\nЗдоровье: {pers.Hp}\n" +
                                                $"Урон: {pers.MinAttack}\nБроня: {pers.Armor}\nЗолото: {pers.Cash}");
                                        Thread.Sleep(1000);//замедляем время вывода.
                                        botClient.SendTextMessageAsync(chat.Id, $"Орк:\nЗдоровье: {yrykhi.Hp}\n" +
                                                $"Урон: {yrykhi.MinAttack}\nБроня: {yrykhi.Armor}");
                                        var inlineKeyboard = new InlineKeyboardMarkup(
                                           new List<InlineKeyboardButton[]>() // здесь создаем лист (массив),который содрежит в себе массив из класса кнопок 
                                           {               // Каждый новый массив - это дополнительные строки,а каждая дополнительная кнопка в массиве - это добавление ряда                    

                                                   new InlineKeyboardButton[] // тут создаем массив кнопок
                                                   {
                                                       InlineKeyboardButton.WithCallbackData("Удар!", "button6"),
                                                       InlineKeyboardButton.WithCallbackData("Инвентарь...", "button7"),
                                                   },

                                           });
                                        Thread.Sleep(1000);//замедляем время вывода.


                                        chek++;
                                        botClient.SendTextMessageAsync(chat.Id, $"{chek}РАУНД!"
                                          , replyMarkup: inlineKeyboard);
                                        return;
                                    }




                                }

                            case "button7":
                                {
                                    botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "Идем в инвентарь!", showAlert: true);

                                    botClient.SendTextMessageAsync(chat.Id, $"{pers.Name} идет в инвентарь!");

                                    if (pers.item.Count != 0)
                                    {
                                        Thread.Sleep(1000);//замедляем время вывода.
                                        botClient.SendTextMessageAsync(chat.Id, $"Ник: {pers.Name}\nЗдоровье: {pers.Hp}\n" +
                                                $"Урон: {pers.MinAttack}\nБроня: {pers.Armor}\nЗолото: {pers.Cash}");
                                        for (int i = 0; i < pers.item.Count; i++)
                                        {
                                            string tmp = " ";
                                            if (pers.item[i].Use == true) tmp = "Используется!";
                                            else if (pers.item[i].Use == false) tmp = "Не используется!";                                           
                                            if (pers.item[i].ItemText == "Склянка здоровья")
                                            {
                                                botClient.SendTextMessageAsync(chat.Id, $"{pers.item[i]._typeOfItems}Цена:{pers.item[i].Cost}\nУрон:{pers.item[i]._damage}\n" +
                                                $"Защита:{pers.item[i]._protection}\nИспользование:{tmp}\nИнформация:{pers.item[i]._info}\n" +
                                                $"Применить жми:/apply{pers.item[i]._typeOfItems}");
                                            }
                                            else
                                            {
                                                botClient.SendTextMessageAsync(chat.Id, $"{pers.item[i]._typeOfItems}Цена:{pers.item[i].Cost}\nУрон:{pers.item[i]._damage}\n" +
                                                $"Защита:{pers.item[i]._protection}\nИспользование:{tmp}\nИнформация:{pers.item[i]._info}");
                                            }
                                        }
                                    }

                                    else botClient.SendTextMessageAsync(chat.Id, "Инвентарь пуст!");

                                    return;
                                }

                        }

                        return;
                    }
                case UpdateType.Message:
                    {
                        var message = update.Message; // Эта переменная будет содержать в себе все связанное с сообщениями

                        var user = message.From; //- это от кого пришло сообщение (или любой другой Update)

                        Console.WriteLine($"{user.FirstName} ({user.Id}) написал сообщение: {message.Text}");

                        var chat = message.Chat; //- содержит всю информацию о чате

                        var name = message.Text;

                        switch (message.Type) // Добавляем проверку на тип Message
                        {

                            case MessageType.Text:  // Тут понятно, текстовый тип
                                {
                                    if (message.Text == "/Attak")
                                    {
                                        // В этом типе клавиатуры обязательно нужно использовать следующий метод
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "В бой!!", showAlert: true);
                                        // Для того, чтобы отправить телеграмму запрос, что мы нажали на кнопку

                                        botClient.SendTextMessageAsync(chat.Id, $"{pers.Name} не думая бросается на Орка из кустов!!!");

                                        Thread.Sleep(1000);
                                        botClient.SendTextMessageAsync(chat.Id, $"1 РАУНД!");

                                        var inlineKeyboard = new InlineKeyboardMarkup(
                                           new List<InlineKeyboardButton[]>() // здесь создаем лист (массив),который содрежит в себе массив из класса кнопок 
                                           {               // Каждый новый массив - это дополнительные строки,а каждая дополнительная кнопка в массиве - это добавление ряда                    

                                                   new InlineKeyboardButton[] // тут создаем массив кнопок
                                                   {
                                                       InlineKeyboardButton.WithCallbackData("Удар!", "button6"),
                                                       InlineKeyboardButton.WithCallbackData("Инвентарь...", "button7"),
                                                   },

                                           });

                                        Thread.Sleep(1000);//замедляем время вывода.
                                        botClient.SendTextMessageAsync(chat.Id, $"Выбор за тобой!!!:"
                                          , replyMarkup: inlineKeyboard);//Все клавиатуры передаются в параметр replyMarkup

                                        return;
                                    }
                                    else if (message.Text == "/Escape")
                                    {
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Сбежать!", showAlert: true);

                                        bool esc = MetodsForPlay.EscapeLuck();

                                        if (esc == true)
                                        {
                                            Thread.Sleep(3000);
                                            botClient.SendTextMessageAsync(chat.Id, $"{pers.Name} тебе удалось умыкнуть от Орка\n" +
                                                $"Но и золота ты не получил.)");
                                            shop = true;
                                            RoadTwo = false;

                                            var inlineKeyboard = new InlineKeyboardMarkup(
                                               new List<InlineKeyboardButton[]>() // здесь создаем лист (массив),который содрежит в себе массив из класса кнопок 
                                               {               // Каждый новый массив - это дополнительные строки,а каждая дополнительная кнопка в массиве - это добавление ряда                    

                                                   new InlineKeyboardButton[] // тут создаем массив кнопок
                                                   {
                                                      InlineKeyboardButton.WithCallbackData("В магазин", "button1"),
                                                      InlineKeyboardButton.WithCallbackData("В бой", "button2"),
                                                      InlineKeyboardButton.WithCallbackData("В инвентарь", "button3"),
                                                   },

                                               });

                                            Thread.Sleep(1000);//замедляем время вывода.
                                            botClient.SendTextMessageAsync(chat.Id, $"Можно и передохнуть!:"
                                              , replyMarkup: inlineKeyboard);//Все клавиатуры передаются в параметр replyMarkup
                                            
                                            return;
                                        }
                                        else
                                        {
                                            Thread.Sleep(3000);
                                            botClient.SendTextMessageAsync(chat.Id, $"Орк:{yrykhi.WarCry}\n{pers.Name} тебе не удалось умыкнуть от орка\n" +
                                                $"Орк заметил тебя. Готовся!!!!)");

                                            Thread.Sleep(1000);
                                            botClient.SendTextMessageAsync(chat.Id, $"1 РАУНД!");

                                            var inlineKeyboard = new InlineKeyboardMarkup(
                                            new List<InlineKeyboardButton[]>() // здесь создаем лист (массив),который содрежит в себе массив из класса кнопок 
                                            {               // Каждый новый массив - это дополнительные строки,а каждая дополнительная кнопка в массиве - это добавление ряда                    

                                                   new InlineKeyboardButton[] // тут создаем массив кнопок
                                                   {
                                                       InlineKeyboardButton.WithCallbackData("Удар!", "button6"),
                                                       InlineKeyboardButton.WithCallbackData("Инвентарь...", "button7"),
                                                   },

                                            });

                                            Thread.Sleep(1000);//замедляем время вывода.
                                            botClient.SendTextMessageAsync(chat.Id, $"Орк уже бежит на Вас!"
                                              , replyMarkup: inlineKeyboard);//Все клавиатуры передаются в параметр replyMarkup
                                        }

                                        return;
                                    }

                                    if (message.Text == "/applyBottleOfHealth")
                                    {
                                        pers.Hp = 100;
                                        Item bottle = RealItems.CreateBottle();
                                        botClient.SendTextMessageAsync(chat.Id, Inventar.DeleteItem(pers, bottle));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Склянка использована!", showAlert: true);

                                    }


                                    return;

                                }

                            default: // Добавил default , чтобы показать вам разницу типов Message
                                {
                                    botClient.SendTextMessageAsync(chat.Id, "Используй только кнопки!");
                                    return;
                                }
                        }


                    }




            }


        }

        public static void RoadThree(ITelegramBotClient botClient, Update update, CancellationToken token,Personage pers,Personage pers2, ref bool shop, ref bool RoadThree,ref bool PVP, ref bool RoadOne)
        {
            Thread.Sleep(1000);

            switch (update.Type) // Сразу же ставим конструкцию switch, чтобы обрабатывать приходящие Update
            {
                case UpdateType.CallbackQuery:
                    {

                        var callbackQuery = update.CallbackQuery;// Переменная,которая будет содержать в себе всю информацию о кнопке,
                                                                 // которую нажали.                         
                        var user = callbackQuery.From;// Аналогично и с Message мы можем получить информацию о чате, о пользователе и т.д.

                        // Выводим на экран нажатие кнопки
                        Console.WriteLine($"{user.FirstName} ({user.Id}) нажал на кнопку: {callbackQuery.Data}");

                        // Вот тут нужно уже быть немножко внимательным и не путаться!
                        // Мы пишем не callbackQuery.Chat , а callbackQuery.Message.Chat , так как
                        // кнопка привязана к сообщению, то мы берем информацию от сообщения.
                        var chat = callbackQuery.Message.Chat;

                        // Добавляем блок switch для проверки кнопок
                        switch (callbackQuery.Data)
                        {
                            // Data - это придуманный нами id кнопки, мы его указывали в параметре
                            // callbackData при создании кнопок. У меня это button1, button2 и button3

                            case "button8":
                                {

                                    botClient.SendTextMessageAsync(chat.Id, $"Удар!!");

                                    smayg.Hp = smayg.Hp - pers.GetAttack(pers.MinAttack, pers.MaxAttack, pers.GetUseItemAtPers(pers));

                                    pers.Hp = pers.Hp - smayg.MinAttack;

                                    if (smayg.Hp <= 0)
                                    {

                                        botClient.AnswerCallbackQueryAsync(callbackQuery.Id, $"{smayg.DeadCry}", showAlert: true);
                                        botClient.SendTextMessageAsync(chat.Id, "Дракон убит!");
                                        Thread.Sleep(1000);//замедляем время вывода.

                                        botClient.SendTextMessageAsync(chat.Id, "У Дракона слетае голова!\n" +
                                            "При обыске вы находите награду!");
                                        botClient.AnswerCallbackQueryAsync(callbackQuery.Id, $"Золото:{smayg.Cash}", showAlert: true);

                                        pers.Cash += smayg.Cash;


                                        chek = 1;
                                        PVP = true;
                                        shop = true;
                                        RoadThree = false;
                                        RoadOne = true;

                                        pers.Active = false;


                                        var inlineKeyboard = new InlineKeyboardMarkup(
                                           new List<InlineKeyboardButton[]>() // здесь создаем лист (массив),который содрежит в себе массив из класса кнопок 
                                           {               // Каждый новый массив - это дополнительные строки,а каждая дополнительная кнопка в массиве - это добавление ряда                    

                                                   new InlineKeyboardButton[] // тут создаем массив кнопок
                                                   {
                                                      InlineKeyboardButton.WithCallbackData("В магазин", "button1"),
                                                      InlineKeyboardButton.WithCallbackData("В бой", "button2"),
                                                      InlineKeyboardButton.WithCallbackData("В инвентарь", "button3"),
                                                   },

                                           });

                                        Thread.Sleep(1000);//замедляем время вывода.
                                        botClient.SendTextMessageAsync(chat.Id, $"Перед тем как передать ход подготовьтесь ко встече с соперником!:"
                                          , replyMarkup: inlineKeyboard);//Все клавиатуры передаются в параметр replyMarkup

                                        return;

                                    }
                                    else if (pers.Hp <= 0)
                                    {
                                        botClient.AnswerCallbackQueryAsync(callbackQuery.Id, $"{pers.IsDie}", showAlert: true);
                                        botClient.SendTextMessageAsync(chat.Id, "Вы убиты!");
                                        return;
                                    }
                                    else
                                    {
                                        Thread.Sleep(1000);//замедляем время вывода.
                                        botClient.SendTextMessageAsync(chat.Id, $"Ник: {pers.Name}\nЗдоровье: {pers.Hp}\n" +
                                                $"Урон: {pers.MinAttack}\nБроня: {pers.Armor}\nЗолото: {pers.Cash}");
                                        Thread.Sleep(1000);//замедляем время вывода.
                                        botClient.SendTextMessageAsync(chat.Id, $"Дракон:\nЗдоровье: {smayg.Hp}\n" +
                                                $"Урон: {smayg.MinAttack}\nБроня: {smayg.Armor}");
                                        var inlineKeyboard = new InlineKeyboardMarkup(
                                           new List<InlineKeyboardButton[]>() // здесь создаем лист (массив),который содрежит в себе массив из класса кнопок 
                                           {               // Каждый новый массив - это дополнительные строки,а каждая дополнительная кнопка в массиве - это добавление ряда                    

                                                   new InlineKeyboardButton[] // тут создаем массив кнопок
                                                   {
                                                       InlineKeyboardButton.WithCallbackData("Удар!", "button8"),
                                                       InlineKeyboardButton.WithCallbackData("Инвентарь...", "button9"),
                                                   },

                                           });
                                        Thread.Sleep(1000);//замедляем время вывода.


                                        chek++;
                                        botClient.SendTextMessageAsync(chat.Id, $"{chek}РАУНД!"
                                          , replyMarkup: inlineKeyboard);
                                        return;
                                    }




                                }

                            case "button9":
                                {
                                    botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "Идем в инвентарь!", showAlert: true);

                                    botClient.SendTextMessageAsync(chat.Id, $"{pers.Name} идет в инвентарь!");

                                    if (pers.item.Count != 0)
                                    {
                                        Thread.Sleep(1000);//замедляем время вывода.
                                        botClient.SendTextMessageAsync(chat.Id, $"Ник: {pers.Name}\nЗдоровье: {pers.Hp}\n" +
                                                $"Урон: {pers.MinAttack}\nБроня: {pers.Armor}\nЗолото: {pers.Cash}");
                                        for (int i = 0; i < pers.item.Count; i++)
                                        {
                                            string tmp = " ";
                                            if (pers.item[i].Use == true) tmp = "Используется!";
                                            else if (pers.item[i].Use == false) tmp = "Не используется!";
                                            if (pers.item[i].ItemText == "Склянка здоровья")
                                            {
                                                botClient.SendTextMessageAsync(chat.Id, $"{pers.item[i]._typeOfItems}Цена:{pers.item[i].Cost}\nУрон:{pers.item[i]._damage}\n" +
                                                $"Защита:{pers.item[i]._protection}\nИспользование:{tmp}\nИнформация:{pers.item[i]._info}\n" +
                                                $"Применить жми:/apply{pers.item[i]._typeOfItems}");
                                            }
                                            else
                                            {
                                                botClient.SendTextMessageAsync(chat.Id, $"{pers.item[i]._typeOfItems}Цена:{pers.item[i].Cost}\nУрон:{pers.item[i]._damage}\n" +
                                                $"Защита:{pers.item[i]._protection}\nИспользование:{tmp}\nИнформация:{pers.item[i]._info}");
                                            }
                                        }
                                    }

                                    else botClient.SendTextMessageAsync(chat.Id, "Инвентарь пуст!");

                                    return;
                                }

                        }

                        return;
                    }
                case UpdateType.Message:
                    {
                        var message = update.Message; // Эта переменная будет содержать в себе все связанное с сообщениями

                        var user = message.From; //- это от кого пришло сообщение (или любой другой Update)

                        Console.WriteLine($"{user.FirstName} ({user.Id}) написал сообщение: {message.Text}");

                        var chat = message.Chat; //- содержит всю информацию о чате

                        var name = message.Text;

                        switch (message.Type) // Добавляем проверку на тип Message
                        {

                            case MessageType.Text:  // Тут понятно, текстовый тип
                                {
                                    if (message.Text == "/Attak")
                                    {
                                        // В этом типе клавиатуры обязательно нужно использовать следующий метод
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "В бой!!", showAlert: true);
                                        // Для того, чтобы отправить телеграмму запрос, что мы нажали на кнопку

                                        botClient.SendTextMessageAsync(chat.Id, $"{pers.Name} не думая бросается на Дракона со сколы!!!");

                                        Thread.Sleep(1000);
                                        botClient.SendTextMessageAsync(chat.Id, $"1 РАУНД!");

                                        var inlineKeyboard = new InlineKeyboardMarkup(
                                           new List<InlineKeyboardButton[]>() // здесь создаем лист (массив),который содрежит в себе массив из класса кнопок 
                                           {               // Каждый новый массив - это дополнительные строки,а каждая дополнительная кнопка в массиве - это добавление ряда                    

                                                   new InlineKeyboardButton[] // тут создаем массив кнопок
                                                   {
                                                       InlineKeyboardButton.WithCallbackData("Удар!", "button8"),
                                                       InlineKeyboardButton.WithCallbackData("Инвентарь...", "button9"),
                                                   },

                                           });

                                        Thread.Sleep(1000);//замедляем время вывода.
                                        botClient.SendTextMessageAsync(chat.Id, $"Выбор за тобой!!!:"
                                          , replyMarkup: inlineKeyboard);//Все клавиатуры передаются в параметр replyMarkup

                                        return;
                                    }
                                    else if (message.Text == "/Escape")
                                    {
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Сбежать!", showAlert: true);

                                        bool esc = MetodsForPlay.EscapeLuck();

                                        if (esc == true)
                                        {
                                            Thread.Sleep(3000);
                                            botClient.SendTextMessageAsync(chat.Id, $"{pers.Name} тебе удалось умыкнуть от Дракона\n" +
                                                $"Но и золота ты не получил.)");
                                            shop = true;
                                            RoadThree = false;

                                            var inlineKeyboard = new InlineKeyboardMarkup(
                                               new List<InlineKeyboardButton[]>() // здесь создаем лист (массив),который содрежит в себе массив из класса кнопок 
                                               {               // Каждый новый массив - это дополнительные строки,а каждая дополнительная кнопка в массиве - это добавление ряда                    

                                                   new InlineKeyboardButton[] // тут создаем массив кнопок
                                                   {
                                                      InlineKeyboardButton.WithCallbackData("В магазин", "button1"),
                                                      InlineKeyboardButton.WithCallbackData("В бой", "button2"),
                                                      InlineKeyboardButton.WithCallbackData("В инвентарь", "button3"),
                                                   },

                                               });

                                            Thread.Sleep(1000);//замедляем время вывода.
                                            botClient.SendTextMessageAsync(chat.Id, $"Можно и передохнуть!:"
                                              , replyMarkup: inlineKeyboard);//Все клавиатуры передаются в параметр replyMarkup

                                            return;
                                        }
                                        else
                                        {
                                            Thread.Sleep(3000);
                                            botClient.SendTextMessageAsync(chat.Id, $"Дракон:{smayg.WarCry}\n{pers.Name} тебе не удалось умыкнуть от Дракона\n" +
                                                $"Дракон заметил тебя. Готовся!!!!)");

                                            Thread.Sleep(1000);
                                            botClient.SendTextMessageAsync(chat.Id, $"1 РАУНД!");

                                            var inlineKeyboard = new InlineKeyboardMarkup(
                                            new List<InlineKeyboardButton[]>() // здесь создаем лист (массив),который содрежит в себе массив из класса кнопок 
                                            {               // Каждый новый массив - это дополнительные строки,а каждая дополнительная кнопка в массиве - это добавление ряда                    

                                                   new InlineKeyboardButton[] // тут создаем массив кнопок
                                                   {
                                                       InlineKeyboardButton.WithCallbackData("Удар!", "button8"),
                                                       InlineKeyboardButton.WithCallbackData("Инвентарь...", "button9"),
                                                   },

                                            });

                                            Thread.Sleep(1000);//замедляем время вывода.
                                            botClient.SendTextMessageAsync(chat.Id, $"Дракон стремительно летит на Вас!"
                                              , replyMarkup: inlineKeyboard);//Все клавиатуры передаются в параметр replyMarkup
                                        }

                                        return;
                                    }

                                    if (message.Text == "/applyBottleOfHealth")
                                    {
                                        pers.Hp = 100;
                                        Item bottle = RealItems.CreateBottle();
                                        botClient.SendTextMessageAsync(chat.Id, Inventar.DeleteItem(pers, bottle));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Склянка использована!", showAlert: true);

                                    }


                                    return;

                                }

                            default: // Добавил default , чтобы показать вам разницу типов Message
                                {
                                    botClient.SendTextMessageAsync(chat.Id, "Используй только кнопки!");
                                    return;
                                }
                        }


                    }




            }
        }

        public static void PVPButtle(ITelegramBotClient botClient, Update update, CancellationToken token, Personage pers1, Personage pers2, ref bool shop, ref bool PVP, ref bool final)
        {
            Thread.Sleep(1000);

            switch (update.Type) // Сразу же ставим конструкцию switch, чтобы обрабатывать приходящие Update
            {
                case UpdateType.CallbackQuery:
                    {

                        var callbackQuery = update.CallbackQuery;// Переменная,которая будет содержать в себе всю информацию о кнопке,
                                                                 // которую нажали.                         
                        var user = callbackQuery.From;// Аналогично и с Message мы можем получить информацию о чате, о пользователе и т.д.

                        // Выводим на экран нажатие кнопки
                        Console.WriteLine($"{user.FirstName} ({user.Id}) нажал на кнопку: {callbackQuery.Data}");

                        // Вот тут нужно уже быть немножко внимательным и не путаться!
                        // Мы пишем не callbackQuery.Chat , а callbackQuery.Message.Chat , так как
                        // кнопка привязана к сообщению, то мы берем информацию от сообщения.
                        var chat = callbackQuery.Message.Chat;

                        // Добавляем блок switch для проверки кнопок
                        switch (callbackQuery.Data)
                        {
                            // Data - это придуманный нами id кнопки, мы его указывали в параметре
                            // callbackData при создании кнопок. У меня это button1, button2 и button3

                            case "button10":
                                {

                                    botClient.SendTextMessageAsync(chat.Id, $"Удар!!");

                                    Program.pers1.Hp = Program.pers1.Hp - Program.pers2.GetAttack(pers2.MinAttack, pers2.MaxAttack, pers2.GetUseItemAtPers(pers2));

                                    Program.pers2.Hp = Program.pers2.Hp - Program.pers1.GetAttack(pers1.MinAttack, pers1.MaxAttack, pers1.GetUseItemAtPers(pers1));

                                    if (pers1.Hp <= 0)
                                    {

                                        botClient.AnswerCallbackQueryAsync(callbackQuery.Id, $"{pers1.IsDie}", showAlert: true);
                                        botClient.SendTextMessageAsync(chat.Id, $"{pers1.Name} убит!");
                                        Thread.Sleep(1000);//замедляем время вывода.

                                        botClient.SendTextMessageAsync(chat.Id, $"У {pers1.Name} башка разлетелась Нахуй!!\n" +
                                            "При обыске вы находите фалос в жопе!");
                                        botClient.AnswerCallbackQueryAsync(callbackQuery.Id, $"Золото:{pers1.Cash}", showAlert: true);

                                        pers2.Cash += pers1.Cash;

                                        PVP = false;
                                        final = true;
                            

                                        Thread.Sleep(1000);//замедляем время вывода.
                                        botClient.SendTextMessageAsync(chat.Id, $"Показать статистику игры.\n Жми: /Stata");

                                        return;

                                    }
                                    else if (pers2.Hp <= 0)
                                    {
                                        botClient.AnswerCallbackQueryAsync(callbackQuery.Id, $"{pers2.IsDie}", showAlert: true);
                                        botClient.SendTextMessageAsync(chat.Id, $"{pers2.Name} убит!");
                                        Thread.Sleep(1000);//замедляем время вывода.

                                        botClient.SendTextMessageAsync(chat.Id, $"У {pers2.Name} башка разлетелась Нахуй!!\n" +
                                            "При обыске вы находите фалос в жопе!");
                                        botClient.AnswerCallbackQueryAsync(callbackQuery.Id, $"Золото:{pers2.Cash}", showAlert: true);

                                        pers1.Cash += pers2.Cash;
                                        PVP = false;
                                        final = true;

                                        Thread.Sleep(1000);//замедляем время вывода.
                                        botClient.SendTextMessageAsync(chat.Id, $"Показать статистику игры.\n Жми: /Stata");

                                        return;
                                    }
                                    else
                                    {
                                        Thread.Sleep(1000);//замедляем время вывода.
                                        botClient.SendTextMessageAsync(chat.Id, $"Ник: {pers1.Name}\nЗдоровье: {pers1.Hp}\n" +
                                                $"Урон: {pers1.MinAttack}\nБроня: {pers1.Armor}\nЗолото: {pers1.Cash}");
                                        Thread.Sleep(1000);//замедляем время вывода.
                                        botClient.SendTextMessageAsync(chat.Id, $"Ник: {pers2.Name}\nЗдоровье: {pers2.Hp}\n" +
                                                $"Урон: {pers2.MinAttack}\nБроня: {pers2.Armor}\nЗолото: {pers2.Cash}");
                                        var inlineKeyboard = new InlineKeyboardMarkup(
                                           new List<InlineKeyboardButton[]>() // здесь создаем лист (массив),который содрежит в себе массив из класса кнопок 
                                           {               // Каждый новый массив - это дополнительные строки,а каждая дополнительная кнопка в массиве - это добавление ряда                    

                                                   new InlineKeyboardButton[] // тут создаем массив кнопок
                                                   {
                                                       InlineKeyboardButton.WithCallbackData("Удар!", "button10"),
                                                       InlineKeyboardButton.WithCallbackData($"Инвентарь{pers1}", "button11"),
                                                       InlineKeyboardButton.WithCallbackData($"Инвентарь{pers2}", "button12"),

                                                   },

                                           });
                                        Thread.Sleep(1000);//замедляем время вывода.


                                        chek++;
                                        botClient.SendTextMessageAsync(chat.Id, $"{chek}РАУНД!"
                                          , replyMarkup: inlineKeyboard);
                                        return;
                                    }




                                }

                            case "button11":
                                {
                                    botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "Идем в инвентарь!", showAlert: true);

                                    botClient.SendTextMessageAsync(chat.Id, $"{pers1.Name} идет в инвентарь!");

                                    if (pers1.item.Count != 0)
                                    {
                                        Thread.Sleep(1000);//замедляем время вывода.
                                        botClient.SendTextMessageAsync(chat.Id, $"Ник: {pers1.Name}\nЗдоровье: {pers1.Hp}\n" +
                                                $"Урон: {pers1.MinAttack}\nБроня: {pers1.Armor}\nЗолото: {pers1.Cash}");
                                        for (int i = 0; i < pers1.item.Count; i++)
                                        {
                                            string tmp = " ";
                                            if (pers1.item[i].Use == true) tmp = "Используется!";
                                            else if (pers1.item[i].Use == false) tmp = "Не используется!";
                                            if (pers1.item[i].ItemText == "Склянка здоровья")
                                            {
                                                botClient.SendTextMessageAsync(chat.Id, $"{pers1.item[i]._typeOfItems}Цена:{pers1.item[i].Cost}\nУрон:{pers1.item[i]._damage}\n" +
                                                $"Защита:{pers1.item[i]._protection}\nИспользование:{tmp}\nИнформация:{pers1.item[i]._info}\n" +
                                                $"Применить жми:/apply{pers1.item[i]._typeOfItems}1");
                                            }
                                            else
                                            {
                                                botClient.SendTextMessageAsync(chat.Id, $"{pers1.item[i]._typeOfItems}Цена:{pers1.item[i].Cost}\nУрон:{pers1.item[i]._damage}\n" +
                                                $"Защита:{pers1.item[i]._protection}\nИспользование:{tmp}\nИнформация:{pers1.item[i]._info}");
                                            }
                                        }
                                    }

                                    else botClient.SendTextMessageAsync(chat.Id, "Инвентарь пуст!");

                                    return;
                                }
                            case "button12":
                                {
                                    botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "Идем в инвентарь!", showAlert: true);

                                    botClient.SendTextMessageAsync(chat.Id, $"{pers2.Name} идет в инвентарь!");

                                    if (pers1.item.Count != 0)
                                    {
                                        Thread.Sleep(1000);//замедляем время вывода.
                                        botClient.SendTextMessageAsync(chat.Id, $"Ник: {pers2.Name}\nЗдоровье: {pers2.Hp}\n" +
                                                $"Урон: {pers2.MinAttack}\nБроня: {pers2.Armor}\nЗолото: {pers2.Cash}");
                                        for (int i = 0; i < pers2.item.Count; i++)
                                        {
                                            string tmp = " ";
                                            if (pers2.item[i].Use == true) tmp = "Используется!";
                                            else if (pers2.item[i].Use == false) tmp = "Не используется!";
                                            if (pers2.item[i].ItemText == "Склянка здоровья")
                                            {
                                                botClient.SendTextMessageAsync(chat.Id, $"{pers2.item[i]._typeOfItems}Цена:{pers2.item[i].Cost}\nУрон:{pers2.item[i]._damage}\n" +
                                                $"Защита:{pers2.item[i]._protection}\nИспользование:{tmp}\nИнформация:{pers2.item[i]._info}\n" +
                                                $"Применить жми:/apply{pers2.item[i]._typeOfItems}1");
                                            }
                                            else
                                            {
                                                botClient.SendTextMessageAsync(chat.Id, $"{pers2.item[i]._typeOfItems}Цена:{pers2.item[i].Cost}\nУрон:{pers2.item[i]._damage}\n" +
                                                $"Защита:{pers2.item[i]._protection}\nИспользование:{tmp}\nИнформация:{pers2.item[i]._info}");
                                            }
                                        }
                                    }

                                    else botClient.SendTextMessageAsync(chat.Id, "Инвентарь пуст!");

                                    return;
                                }

                        }

                        return;
                    }
                case UpdateType.Message:
                    {
                        var message = update.Message; // Эта переменная будет содержать в себе все связанное с сообщениями

                        var user = message.From; //- это от кого пришло сообщение (или любой другой Update)

                        Console.WriteLine($"{user.FirstName} ({user.Id}) написал сообщение: {message.Text}");

                        var chat = message.Chat; //- содержит всю информацию о чате

                        var name = message.Text;

                        switch (message.Type) // Добавляем проверку на тип Message
                        {

                            case MessageType.Text:  // Тут понятно, текстовый тип
                                {
                                    if (message.Text == "/Attak")
                                    {
                                        // В этом типе клавиатуры обязательно нужно использовать следующий метод
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "В бой!!", showAlert: true);
                                        // Для того, чтобы отправить телеграмму запрос, что мы нажали на кнопку

                                        botClient.SendTextMessageAsync(chat.Id, $"{pers1.Name} и {pers2.Name} начали жестко долбиться!!!");

                                        Thread.Sleep(1000);
                                        botClient.SendTextMessageAsync(chat.Id, $"1 РАУНД!");

                                        var inlineKeyboard = new InlineKeyboardMarkup(
                                           new List<InlineKeyboardButton[]>() // здесь создаем лист (массив),который содрежит в себе массив из класса кнопок 
                                           {               // Каждый новый массив - это дополнительные строки,а каждая дополнительная кнопка в массиве - это добавление ряда                    

                                                   new InlineKeyboardButton[] // тут создаем массив кнопок
                                                   {
                                                       InlineKeyboardButton.WithCallbackData("Удар!", "button10"),
                                                       InlineKeyboardButton.WithCallbackData($"Инвентарь{pers1}", "button11"),
                                                       InlineKeyboardButton.WithCallbackData($"Инвентарь{pers2}", "button12"),
                                                   },

                                           });

                                        Thread.Sleep(1000);//замедляем время вывода.
                                        botClient.SendTextMessageAsync(chat.Id, $"Выбор за тобой!!!:"
                                          , replyMarkup: inlineKeyboard);//Все клавиатуры передаются в параметр replyMarkup

                                        return;
                                    }
                                   

                                    if (message.Text == "/applyBottleOfHealth1")
                                    {
                                        pers1.Hp = 100;
                                        Item bottle = RealItems.CreateBottle();
                                        botClient.SendTextMessageAsync(chat.Id, Inventar.DeleteItem(pers1, bottle));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Склянка использована!", showAlert: true);

                                    }
                                    else if (message.Text == "/applyBottleOfHealth2")
                                    {
                                        pers2.Hp = 100;
                                        Item bottle = RealItems.CreateBottle();
                                        botClient.SendTextMessageAsync(chat.Id, Inventar.DeleteItem(pers2, bottle));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Склянка использована!", showAlert: true);
                                    }


                                        return;

                                }

                            default: // Добавил default , чтобы показать вам разницу типов Message
                                {
                                    botClient.SendTextMessageAsync(chat.Id, "Используй только кнопки!");
                                    return;
                                }
                        }


                    }




            }
        }

        public static void StatisticButtle(ITelegramBotClient botClient, Update update, CancellationToken token,Personage pers1,Personage pers2)
        {


            switch (update.Type) // Сразу же ставим конструкцию switch, чтобы обрабатывать приходящие Update
            {

                case UpdateType.Message:
                    {
                        var message = update.Message; // Эта переменная будет содержать в себе все связанное с сообщениями

                        var user = message.From; //- это от кого пришло сообщение (или любой другой Update)

                        Console.WriteLine($"{user.FirstName} ({user.Id}) написал сообщение: {message.Text}");

                        var chat = message.Chat; //- содержит всю информацию о чате

                        var name = message.Text;

                        switch (message.Type) // Добавляем проверку на тип Message
                        {

                            case MessageType.Text:  // Тут понятно, текстовый тип
                                {
                                    if (message.Text == "/Stata")
                                    {
                                        botClient.SendTextMessageAsync(chat.Id, $"Ник: {pers1.Name}\nЗдоровье: {pers1.Hp}\n" +
                                            $"Урон: {pers1.MinAttack}\nБроня: {pers1.Armor}\nЗолото: {pers1.Cash}");

                                        botClient.SendTextMessageAsync(chat.Id, $"Ник: {pers2.Name}\nЗдоровье: {pers2.Hp}\n" +
                                            $"Урон: {pers2.MinAttack}\nБроня: {pers2.Armor}\nЗолото: {pers2.Cash}");

                                        return;
                                    }

                                    return;

                                }

                            default: // Добавил default , чтобы показать вам разницу типов Message
                                {
                                    botClient.SendTextMessageAsync(chat.Id, "Используй только кнопки!");
                                    return;
                                }
                        }


                    }




            }
        }


    }


}
