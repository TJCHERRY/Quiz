using System;
using System.Collections.Generic;

//class that defines the construct of each element in the target json file
[Serializable]
public class QuizDataSet
{
    public string question;
    public string[] options;
    public int correctOptionIndex;
}

//class containing list field of type"QuizDataSet" which will store the contents of the entire target json file
[Serializable]
public class DataArray
{
    public QuizDataSet[] quizDataList;
}
