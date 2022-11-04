using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Threading.Tasks;

namespace CSharp19
{
    //static클래스


    //dynamic 클래스
    class Duck
    {
        public void Walk()
        {

        }
        public void Swim()
        {

        }
        public void Quack()
        {

        }
    }
    class Mallard : Duck
    {

    }
    class Robot

    {


    }

    internal class Program
    {


        static void Main(string[] args)
        {
            if (false)
            {

                //static 키워드 

                //dynamic 형식
                //int ,float와 같은 데이터 형식이다.
                //일반적으로 형식 검사는 컴파일시에 이루어진다. 하지만 dynamic형식은 실행시에 이루어진다.


                //오리도,청둥오리도,로봇도 할 수 있는 행동은 같다,
                //하지만 컴파일러는 Duck이나 Mallard는 오리로 인정해도 Robot은 인정하지 않는다,

                //dynamic배열. 이렇게 되면 컴파일러가 형식 검사를 런타임으로 미룬다.
                dynamic[] duckArray = { new Duck(), new Mallard(), new Robot() };
                foreach (dynamic duck in duckArray)
                {
                    Console.WriteLine(duck.GetType());
                    duck.Walk();
                    duck.Swim();
                    duck.Quack();
                    Console.WriteLine();

                }

                dynamic a = 10;
                Console.WriteLine($"{a}({a.GetType})");

                a = 3.1415;
                Console.WriteLine($"{a}({a.GetType})");

                a = "ABCDEFG";
                Console.WriteLine($"{a}({a.GetType})");

            }
            if (false)
            {

        /*        dynamic playerInfo = GetDynamicInfo();
                Console.WriteLine($"이름:{playerInfo.name}, 레벨: {playerInfo.level}, 골드:{playerInfo.Gold}");


                dynamic frineds = GetFriendList();
                Console.WriteLine("친구목록");
                Console.WriteLine("---------------------------");
                foreach (dynamic friend in frineds.friendlist)
                    Console.WriteLine(friend);

                Console.WriteLine();
                Console.WriteLine("차단 목록");
                Console.WriteLine("-----------------------------");
                foreach (dynamic black in frineds.blackList)
                    Console.WriteLine(black);*/

            }

            //동기식 
            double deltaTime = 1000 / 12;
            double timer = 0;
            int position = 0;
            int delayTime = 500;
            bool isRun = true;
            string message = string.Empty;
            while (isRun)
            {
                timer += deltaTime;
                if ((timer += deltaTime) >= deltaTime)
                {
                    timer -= delayTime;
                    position += 1;
                    Console.Clear();
                    Console.CursorLeft = position % 3;
                    Console.Write("★");
                    Console.WriteLine(message);
                }

                // 오버해드: 다른 알고리즘으로 인하여 딜레이가 걸리는 현상이다
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.D)
                {
                    message = "다운로드 진행중";
                    Download((isSuccess) =>
                    {
                        Console.Clear();
                        Console.WriteLine($"다운로드 완료 :{isSuccess}");
                        isRun = false;

                    },
                    (int current, int max) =>
                    {
                        message = $"다운로드중...({current / (float)max * 100}%)";

                    });
                    Thread.Sleep((int)deltaTime);
                }

                //await
                //비동기식에서 사용하는 키워드

                static async void Download(Action<bool> onEnd, Action<int, int> onProcess)              //델리게이트
                {
                    var task = Task.Run(() => DownloadHandle(onProcess));                                //비동기식으로 Work Thread에서 또는 Task(작업목록)에서 생성.
                    bool isSuccess = await task;                                                         //task가 끝날때까지 기다렸다가 끝나면 반환형인 bool을 반환(대입)함.
                    onEnd?.Invoke(isSuccess);                                                           //매개변수로 받는 델리게이트 호출          
                    ;
                }

                static bool DownloadHandle(Action<int, int> onProcess)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        onProcess?.Invoke(i + 1, 10);
                        Thread.Sleep(400);
                    }
                    return true;
                }

                static dynamic GetDynamicInfo()
                {
                    //실제 DB에서 검색하는 부분은 제외한다.
                    dynamic info = new { name = "플레이어 1004", level = 250, gold = 2400000 };
                    return info;
                }

                static dynamic GetFriendList()
                {

                    //DB데이터 검색함
                    List<string> friendList = new List<string>(new string[] { ",강승태", "고희준", "김민환", "김희진", "신지은" });
                    List<string> blackList = new List<string>(new string[] { "이경탁", "이기현", "이지웅 ", "장인혁", "최주영", });

                    dynamic info = new { friendList = friendList, blackList = blackList };
                    return info;
                }
            }

        }
    }
}
