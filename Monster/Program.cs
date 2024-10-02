using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Monster
{
    class Program
    {

        private static string token = "7188042963:AAHngz868oZLdhHX1rp-j4Xd1p9PEioTpGY";

        private static ITelegramBotClient _client; // Это клиент для работы с Telegram Bot API, который позволяет отправлять сообщения, управлять ботом, подписываться на обновления и многое другое.
                                                      
        private static ReceiverOptions _receiverOptions; // Это объект с настройками работы бота. Здесь мы будем указывать, какие типы Update мы будем получать, Timeout бота и так далее.

        public static bool onOff = true; // проверка в игре мы или нет.

        public static bool start = true; // проверка начала истории

        public static bool shop = true;  //проверка возможности попадания в магазин

        public static bool HOrS = true; // проверка начала истории

        public static bool RoadOne = true;

        public static bool RoadTwo = false;

        public static bool RoadThree = false;

        public static bool PVP = false;

        public static bool final = false;

        public static Personage pers1 = new Personage();

        public static Personage pers2 = new Personage();
        
       

        static async Task Main(string[] args)
        {
    
            _client = new TelegramBotClient(token); //Привязываем бота к приложению по ключу
            _client.StartReceiving(Update,Error);
            
            await Task.Delay(-1); // Устанавливаем бесконечную задержку, чтобы наш бот работал постоянно         

            

        }   

        private static Task Error(ITelegramBotClient botClient, Exception exeption, CancellationToken token)
        {
            throw new Exception();
        }

        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            pers1.Number = 1;
            pers2.Number = 2;

            var message = update.Message;

            if (pers1.ActiveStart == false) MetodsForPlay.StartGame(botClient, update, token, pers1);

            else if (pers2.ActiveStart == false)
            {
                MetodsForPlay.StartGame(botClient, update, token, pers2);

                if(pers2.WarCry != null) pers1.ActiveStart = true;
            }

            if (pers1.ActiveStart == true && start == true) 
            {
                Hestory.StartText(botClient, update, token, ref HOrS,pers1);

                start = false;
                
            }

            if (shop == true && start == false )
            {
                MetodsForPlay.GetHestoryOrShop(botClient, update, token, pers1, ref shop, ref RoadTwo, ref RoadThree, ref PVP);               
            }

            if(shop == false && RoadOne == true && pers1.Active == true)
            {
                Hestory.RoadOne(botClient, update, token, ref RoadOne, pers1, ref shop, ref RoadOne);
            }

            if (shop == false && RoadTwo == true && pers1.Active == true)
            {
                Hestory.RoadTwo(botClient, update, token, ref RoadTwo, pers1, ref shop, ref RoadTwo);
            }

            if (shop == false && RoadThree == true && pers1.Active == true)
            {
                Hestory.RoadThree(botClient, update, token, pers1,pers2, ref shop, ref RoadThree, ref PVP, ref  RoadOne);

                if (Hestory.smayg.Hp <= 0) pers2.Active = true;
            }



            if (shop == false && RoadOne == true && pers2.Active == true)
            {
                Hestory.RoadOne(botClient, update, token, ref RoadOne, pers2, ref shop, ref RoadOne);
            }

            if (shop == false && RoadTwo == true && pers2.Active == true)
            {
                Hestory.RoadTwo(botClient, update, token, ref RoadTwo, pers2, ref shop, ref RoadTwo);
            }

            if (shop == false && RoadThree == true && pers2.Active == true)
            {
                Hestory.RoadThree(botClient, update, token, pers1, pers2, ref shop, ref RoadThree, ref PVP, ref RoadOne);
            }



            if (shop == false && PVP == true && final == false)
            {
                Hestory.PVPButtle(botClient, update, token,  pers1,  pers2, ref shop, ref PVP, ref final);
            }

            


        }

    }
}
