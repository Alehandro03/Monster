using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Monster
{
    class MetodsForPlay
    {
        public static string NameMonster;

        public static Item qwe = new Item();

        public static bool tmp = true;

        public static bool tmp2 = true;

        public static bool tmp3 = true;
        public static void StartGame(ITelegramBotClient botClient, Update update, CancellationToken token, Personage pers)
        {
            

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

                            case "button1":
                                {
                                    // В этом типе клавиатуры обязательно нужно использовать следующий метод
                                    botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "Отлично ты выбрал рассу - Эльф!", showAlert: true);
                                    // Для того, чтобы отправить телеграмму запрос, что мы нажали на кнопку

                                    if (Enum.IsDefined(typeof(MonsterType), 7)) //Такой проверкой мы узнаем есть ли такое значение под данным номером.
                                    {
                                        pers._monster = (MonsterType)7; //приведем int к MonsterType

                                        pers.Armor = 5;
                                    }

                                    botClient.SendTextMessageAsync(chat.Id, $"Хорошо, {pers.Name} теперь ты {pers.MonsterText}\n" +
                                            $"Выбери свой боевой клич\n(Кримеру: В Атаку!)");

                                    return;
                                }

                            case "button2":
                                {
                                    // А здесь мы добавляем наш сообственный текст, который заменит слово "загрузка", когда мы нажмем на кнопку
                                    botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "Отлично ты выбрал рассу - Человек!", showAlert: true);

                                    if (Enum.IsDefined(typeof(MonsterType), 8)) //Такой проверкой мы узнаем есть ли такое значение под данным номером.
                                    {
                                        pers._monster = (MonsterType)8;

                                        pers.Hp = 130;
                                    }

                                    botClient.SendTextMessageAsync(chat.Id, $"Хорошо, {pers.Name} теперь ты {pers.MonsterText}\n" +
                                            $"Выбери свой боевой клич\n(К примеру: В Атаку!)");
                                    return;
                                }

                            case "button3":
                                {
                                    // А тут мы добавили еще showAlert, чтобы отобразить пользователю полноценное окно
                                    botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "Отлично ты выбрал рассу - Орк!", showAlert: true);

                                    if (Enum.IsDefined(typeof(MonsterType), 2)) //Такой проверкой мы узнаем есть ли такое значение под данным номером.
                                    {
                                        pers._monster = (MonsterType)2;

                                        pers.MinAttack = 5;
                                    }

                                    botClient.SendTextMessageAsync(chat.Id, $"Хорошо, {pers.Name} теперь ты {pers.MonsterText}\n" +
                                            $"Выбери свой боевой клич\n(К примеру: В Атаку!)");
                                    return;
                                }
                            case "button4":
                                {
                                    // А тут мы добавили еще showAlert, чтобы отобразить пользователю полноценное окно
                                    botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "Отлично ты выбрал рассу - Вор!", showAlert: true);

                                    if (Enum.IsDefined(typeof(MonsterType), 4)) //Такой проверкой мы узнаем есть ли такое значение под данным номером.
                                    {
                                        pers._monster = (MonsterType)4;
                                    }

                                    botClient.SendTextMessageAsync(chat.Id, $"Хорошо, {pers.Name} теперь ты {pers.MonsterText}\n" +
                                            $"Выбери свой боевой клич\n(К примеру: В Атаку!)");
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

                                    if (tmp2 == true)// тут обрабатываем команду /start.
                                    {
                                        tmp2 = false;

                                        botClient.SendTextMessageAsync(chat.Id, "Добрый день.\nДля продолжения введите:/start");

                                    }

                                    
                                    if (message.Text == "/start")// тут обрабатываем команду /start.
                                    {
                                        

                                        tmp = false;

                                        botClient.SendTextMessageAsync(chat.Id, $"Добро пожаловать игрок {pers.Number} в мини RPG игру!!!\n" +
                                            "Для продолжения введите свой Nick Name:");

                                        return;

                                    }


                                    if (message.Text == name && pers.Name == null && tmp == false && name != "/start")
                                    {
                                        pers.Name = name;

                                        var inlineKeyboard = new InlineKeyboardMarkup(
                                           new List<InlineKeyboardButton[]>() // здесь создаем лист (массив),который содрежит в себе массив из класса кнопок 
                                           {               // Каждый новый массив - это дополнительные строки,а каждая дополнительная кнопка в массиве - это добавление ряда                    

                                                   new InlineKeyboardButton[] // тут создаем массив кнопок
                                                   {
                                                       InlineKeyboardButton.WithCallbackData("Вор","button4" ),
                                                       InlineKeyboardButton.WithCallbackData("Эльф", "button1"),
                                                       InlineKeyboardButton.WithCallbackData("Человек", "button2"),
                                                       InlineKeyboardButton.WithCallbackData("Орк", "button3"),
                                                   },
 
                                           });


                                        botClient.SendTextMessageAsync(chat.Id, $"{pers.Name}, теперь выбери свои рассу:"
                                          , replyMarkup: inlineKeyboard);//Все клавиатуры передаются в параметр replyMarkup

                                        return;
                                    }

                                    if (message.Text == name && pers._monster != 0 && tmp == false && tmp3 == true)
                                    {
                                        Thread.Sleep(1000);
                                        tmp3 = false;
                                        tmp2 = true;
                                        pers.WarCry = message.Text;

                                        botClient.SendTextMessageAsync(chat.Id, $"И так попреветствуем игрока - {pers.Number}:\n" +
                                        $"Имя:{pers.Name},\nРасса:{pers.MonsterText}\nКоличество здоровья:{pers.Hp}\nБроня:{pers.Armor}\n" +
                                        $"Сила атаки:{pers.MinAttack}\nКолличество золота:{pers.Cash}\n" +
                                        $"Боевой клич:{pers.WarCry}\n");
                                        Thread.Sleep(1000);
                                        if (pers.Number == 1) botClient.SendTextMessageAsync(chat.Id, $"Для продолжения нажмите:/more");
                                                                        
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

        public static List<Item> GetAllListItem()
        {
            List<Item> item = new List<Item>();

            item.Add(RealItems.CreateBottle());

            item.Add(RealItems.CreateOnion());

            item.Add(RealItems.CreateArmor());

            item.Add(RealItems.CreateKnife());

            item.Add(RealItems.CreateHelmet());

            item.Add(RealItems.CreateShield());

            item.Add(RealItems.CreateSword());

            item.Add(RealItems.CreateСlub());

            return item;
        }        

        public static void GetHestoryOrShop(ITelegramBotClient botClient, Update update, CancellationToken token,Personage pers, ref bool shop, ref bool RoadTwo, ref bool RoadThree, ref bool PVP)
        {
            switch (update.Type)
            {
                
                case UpdateType.CallbackQuery:
                    {
                        var callbackQuery = update.CallbackQuery;// Переменная,которая будет содержать в себе всю информацию о кнопке,которую нажали.

                        var user = callbackQuery.From;// Аналогично и с Message мы можем получить информацию о чате, о пользователе и т.д.

                        // Выводим на экран нажатие кнопки
                        Console.WriteLine($"{user.FirstName} ({user.Id}) нажал на кнопку: {callbackQuery.Data}");

                        var chat = callbackQuery.Message.Chat; //- содержит всю информацию о чате

                        switch (callbackQuery.Data)
                        {
                            // Data - это придуманный нами id кнопки, мы его указывали в параметре
                            // callbackData при создании кнопок. У меня это button1, button2 и button3

                            case "button1":
                                {
                                    // В этом типе клавиатуры обязательно нужно использовать следующий метод
                                    botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "Идем в мгазин!", showAlert: true);
                                    // Для того, чтобы отправить телеграмму запрос, что мы нажали на кнопку   

                                    botClient.SendTextMessageAsync(chat.Id, $"{pers.Name} идет в магазин!\nВот список предметов в магазине:");

                                    

                                    List<Item> item = GetAllListItem();
                                    for(int i = 0; i < item.Count; i++)
                                    {                                       
                                        botClient.SendTextMessageAsync(chat.Id, $"{item[i].ItemText}\nЦена:{item[i].Cost}\nУрон:{item[i]._damage}\n" +
                                            $"Защита:{item[i]._protection}\nИнформация:{item[i]._info}\nКупить жми:/buy{i+1}");
                                    }
                                 
                                    return;
                                }

                            case "button2":
                                {
                                    // А здесь мы добавляем наш сообственный текст, который заменит слово "загрузка", когда мы нажмем на кнопку
                                    botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "Идем на тропу!", showAlert: true);                                  

                                    shop = false;

                                    Thread.Sleep(1000);//замедляем время вывода.

                                    if(Hestory.wolf.Hp > 0)
                                    {
                                        NameMonster = Hestory.wolf.Name;
                                        botClient.SendTextMessageAsync(chat.Id, "Вот мы и вышли на тропу. Проходя через густую чащу и увидели в темноте монстра\n" +
                                        $"Скорее всего это {NameMonster} .\nНужно принять рещение:\n Вступить в бой: /Attak\nСбежать(50% успеха): /Escape");
                                    }
                                    else 
                                    if(Hestory.wolf.Hp <= 0 && Hestory.yrykhi.Hp > 0)
                                    {
                                        NameMonster = Hestory.yrykhi.Name;
                                        RoadTwo = true;
                                        botClient.SendTextMessageAsync(chat.Id, "Вот мы и вышли на тропу. Проходя через густую чащу и увидели в темноте монстра\n" +
                                        $"Скорее всего это {NameMonster} .\nНужно принять рещение:\n Вступить в бой: /Attak\nСбежать(50% успеха): /Escape");

                                    }
                                    else if(Hestory.wolf.Hp <= 0 && Hestory.yrykhi.Hp <= 0 && Hestory.smayg.Hp > 0)
                                    {
                                        NameMonster = Hestory.smayg.Name;
                                        RoadThree = true;
                                        botClient.SendTextMessageAsync(chat.Id, "Вот мы и вышли на тропу. Проходя через густую чащу и увидели в темноте монстра\n" +
                                        $"Скорее всего это {NameMonster} .\nНужно принять рещение:\n Вступить в бой: /Attak\nСбежать(50% успеха): /Escape");
                                    }

                                    if(PVP == true)
                                    {
                                        botClient.SendTextMessageAsync(chat.Id, $"Добро пожаловать: {Program.pers1.Name} и {Program.pers2.Name}\n На финальную битву.\n" +
                                            $"Вы убили всех трех монстров, закупились оружием и склянками.\n Пора проверить кто сильнее! ");
                                    }
                                   
                                   
                                    return;

                                }
                            case "button3":
                                {
                                    // А здесь мы добавляем наш сообственный текст, который заменит слово "загрузка", когда мы нажмем на кнопку
                                    botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "Идем в инвентарь!", showAlert: true);

                                    botClient.SendTextMessageAsync(chat.Id, $"{pers.Name} идет в инвентарь!"); //{pers.inventar.ShowInventar(pers.inventar.mas)

                                    if(pers.item.Count != 0)
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
                                                $"Защита:{pers.item[i]._protection}\nИспользование:{tmp}\nИнформация:{pers.item[i]._info}\n" +
                                                $"Применить жми:/apply{pers.item[i]._typeOfItems}\nСнять предмет:/takeOff{pers.item[i]._typeOfItems}");
                                            
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
                                    if(message.Text == "/applyBottleOfHealth")
                                    {                            
                                        pers.Hp = 100;
                                        Item bottle = RealItems.CreateBottle();
                                        botClient.SendTextMessageAsync(chat.Id, Inventar.DeleteItem(pers, bottle));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Склянка использована!", showAlert: true);

                                    }
                                    else if (message.Text == "/applySword")
                                    {
                                        Item sword = RealItems.CreateSword();
                                        botClient.SendTextMessageAsync(chat.Id, Inventar.UseItem(pers, sword));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Меч использован!", showAlert: true);
                                    }
                                    else if(message.Text == "/applyOnion")
                                    {
                                        Item onion = RealItems.CreateOnion();                                       
                                        botClient.SendTextMessageAsync(chat.Id, Inventar.UseItem(pers, onion));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Лук использован!", showAlert: true);
                                    }                              
                                    else if (message.Text == "/applyShield")
                                    {
                                        Item shield = RealItems.CreateShield();
                                        botClient.SendTextMessageAsync(chat.Id, Inventar.UseItem(pers, shield));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Щит использован!", showAlert: true);
                                    }
                                    else if (message.Text == "/applyHelmet")
                                    {
                                        Item helmet = RealItems.CreateHelmet();
                                        botClient.SendTextMessageAsync(chat.Id, Inventar.UseItem(pers, helmet));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Шлем использован!", showAlert: true);
                                    }
                                    else if (message.Text == "/applyArmor")
                                    {
                                        Item armor = RealItems.CreateArmor();
                                        botClient.SendTextMessageAsync(chat.Id, Inventar.UseItem(pers, armor));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Броня использован!", showAlert: true);
                                    }
                                    else if (message.Text == "/applyClub")
                                    {
                                        Item club = RealItems.CreateСlub();
                                        botClient.SendTextMessageAsync(chat.Id, Inventar.UseItem(pers, club));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Дубинка использована!", showAlert: true);
                                    }
                                    else if (message.Text == "/applyKnife")
                                    {
                                        Item knife = RealItems.CreateKnife();
                                        botClient.SendTextMessageAsync(chat.Id, Inventar.UseItem(pers, knife));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Нож использован!", showAlert: true);
                                    }

                                    if(message.Text == "/takeOffSword")
                                    {
                                        Item bottle = RealItems.CreateBottle();
                                        botClient.SendTextMessageAsync(chat.Id, Inventar.DeleteItem(pers, bottle));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Склянка использована!", showAlert: true);
                                    }
                                    else if (message.Text == "/takeOffSword")
                                    {
                                        Item sword = RealItems.CreateSword();
                                        botClient.SendTextMessageAsync(chat.Id, Inventar.RemoveItem(pers, sword));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Меч снят!", showAlert: true);
                                    }
                                    else if (message.Text == "/takeOffOnion")
                                    {
                                        Item onion = RealItems.CreateOnion();
                                        botClient.SendTextMessageAsync(chat.Id, Inventar.RemoveItem(pers, onion));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Лук снят!", showAlert: true);
                                    }
                                    else if (message.Text == "/takeOffShield")
                                    {
                                        Item shield = RealItems.CreateShield();
                                        botClient.SendTextMessageAsync(chat.Id, Inventar.RemoveItem(pers, shield));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Щит снят!", showAlert: true);
                                    }
                                    else if (message.Text == "/takeOffHelmet")
                                    {
                                        Item helmet = RealItems.CreateHelmet();
                                        botClient.SendTextMessageAsync(chat.Id, Inventar.RemoveItem(pers, helmet));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Шлем снят!", showAlert: true);
                                    }
                                    else if (message.Text == "/takeOffArmor")
                                    {
                                        Item armor = RealItems.CreateArmor();
                                        botClient.SendTextMessageAsync(chat.Id, Inventar.RemoveItem(pers, armor));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Броня снята!", showAlert: true);
                                    }
                                    else if (message.Text == "/takeOffClub")
                                    {
                                        Item club = RealItems.CreateСlub();
                                        botClient.SendTextMessageAsync(chat.Id, Inventar.RemoveItem(pers, club));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Дубинка снята!", showAlert: true);
                                    }
                                    else if (message.Text == "/takeOffKnife")
                                    {
                                        Item knife = RealItems.CreateKnife();
                                        botClient.SendTextMessageAsync(chat.Id, Inventar.RemoveItem(pers, knife));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Нож снят!", showAlert: true);
                                    }

                                    if (message.Text == "/buy1")
                                    {
                                        botClient.SendTextMessageAsync(chat.Id, Shop.GetBottle(pers));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Склянка преобретена!", showAlert: true);
                                    }
                                    else if (message.Text == "/buy2")
                                    {
                                        botClient.SendTextMessageAsync(chat.Id, Shop.GetOnion(pers));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Лук преобретен!", showAlert: true);
                                    }
                                    else if (message.Text == "/buy3")
                                    {
                                        botClient.SendTextMessageAsync(chat.Id, Shop.GetArmor(pers));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Доспехи преобретены!", showAlert: true);
                                    }
                                    else if (message.Text == "/buy4")
                                    {
                                        botClient.SendTextMessageAsync(chat.Id, Shop.GetKnife(pers));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Нож преобретен!", showAlert: true);
                                    }
                                    else if (message.Text == "/buy5")
                                    {
                                        botClient.SendTextMessageAsync(chat.Id, Shop.GetHelmet(pers));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Шлем преобретен!", showAlert: true);
                                    }
                                    else if (message.Text == "/buy6")
                                    {
                                        botClient.SendTextMessageAsync(chat.Id, Shop.GetShield(pers));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Щит преобретен!", showAlert: true);
                                    }
                                    else if (message.Text == "/buy7")
                                    {
                                        botClient.SendTextMessageAsync(chat.Id, Shop.GetSword(pers));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Меч преобретен!", showAlert: true);
                                    }
                                    else if (message.Text == "/buy8")
                                    {
                                        botClient.SendTextMessageAsync(chat.Id, Shop.GetClub(pers));
                                        botClient.AnswerCallbackQueryAsync(chat.Id.ToString(), "Дубина преобретена!", showAlert: true);
                                    }


                                    if (message.Text == name)
                                    {
                                        

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
                                        botClient.SendTextMessageAsync(chat.Id, $"Идти в бой,в магазин или в инвентарь?:"
                                          , replyMarkup: inlineKeyboard);//Все клавиатуры передаются в параметр replyMarkup


                                       
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

        public static bool EscapeLuck()
        {
            bool esc;

            Random rnd = new Random();

            int chec = rnd.Next(1, 2);

            if (chec == 1) esc = false;
            else esc = true;

            return esc;
        }

    }
}
