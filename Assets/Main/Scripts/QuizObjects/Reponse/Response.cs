using System;
using System.Collections.Generic;
using UnityEngine;

namespace Quiz
{
    [Serializable]
    public class Question
    {
        public string category;
        public string correctAnswer;
        public string difficulty;
        public string incorrectAnswers1;
        public string question;
        public string type;



        public List<string> possible_answers
        {
            get
            {
                var list = new List<string>();

                list.Add(this.correctAnswer);

                if (!String.IsNullOrEmpty(this.incorrectAnswers1))
                {
                    list.AddRange(this.incorrectAnswers1.Split(' '));
                }
                return list;
            }
        }

        public List<string> incorrect_answers
        {
            get
            {
                var list = new List<string>();

                Debug.Log(this.question);
                Debug.Log(this.incorrectAnswers1);
                if(!String.IsNullOrEmpty(this.incorrectAnswers1))
                {
                    Debug.Log(this.incorrectAnswers1.Split(' '));
                    list.AddRange(this.incorrectAnswers1.Split(' '));
                }
                return list;
            }
        }
    }

   
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>
            {
                Items = array
            };
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }


    [Serializable]
    public class Error
    {
        public long statusCode;
        public string errorMessage;

    }
}
