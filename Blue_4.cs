using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Collections.ObjectModel;

namespace Lab_6
{
    public class Blue_4
    {
        public struct Team
        {
            private string _name;
            private int[] _scores;

            public string Name => _name;
            public int[] Scores
            {
                get
                {
                    if (_scores == null)
                    {
                        return default(int[]);
                    }
                    int[] newScores = new int[_scores.Length];
                    for (int i = 0; i < _scores.Length; i++)
                    {
                        newScores[i] = _scores[i];
                    }
                    return newScores;
                }
            }
            public int TotalScore
            {
                get
                {
                    if (_scores == null || _scores.Length == 0)
                    {
                        return 0;
                    }
                    int total = 0;
                    foreach (int score in _scores)
                    {
                        total += score;
                    }
                    return total;
                }
            }

            public Team(string name)
            {
                _name = name;
                _scores = new int[0];
            }

            public void PlayMatch(int result)
            {
                if (_scores == null)
                {
                    return;
                }
                int[] newScores = new int[_scores.Length + 1];
                Array.Copy(_scores, newScores, _scores.Length);
                newScores[_scores.Length] = result;
                _scores = newScores;
            }

            public void Print()
            {
                Write($"{_name}: {TotalScore}");
                WriteLine("");
            }
        }


        public struct Group
        {
            private string _name;
            private Team[] _teams;
            private int _count;

            public string Name => _name;
            public Team[] Teams
            {
                get
                {
                    if (_teams == null || _teams.Length == 0)
                    {
                        return default(Team[]);
                    }
                    Team[] newTeams = new Team[_teams.Length];
                    for (int i = 0; i < _teams.Length; i++)
                    {
                        newTeams[i] = _teams[i];
                    }
                    return newTeams;
                }
            }

            public Group(string name)
            {
                _name = name;
                _teams = new Team[12];
                _count = 0;
            }
            public void Add(Team team)
            {
                if (_teams == null || _count == 12)
                {
                    return;
                }
                else
                {
                    _teams[_count++] = team;
                }
            }

            public void Add(Team[] teams)
            {
                if (_teams == null){
                    return;
                }
                foreach (Team team in teams)
                {
                    Add(team);
                }
                
            }
            public void Sort()
            {
                if (_teams == null)
                    return;
                var sortedTeams = _teams.OrderByDescending(team => team.TotalScore).ToArray();
                Array.Copy(sortedTeams, _teams, _teams.Length);

            }
            public static Group Merge(Group group1, Group group2, int size)
            {
                if (size <= 0) return default(Group);
                Group result = new Group("Финалисты");
                int i = 0; int j = 0;
                while (i < size / 2 && j < size / 2)
                {
                    if (group1.Teams[i].TotalScore >= group2.Teams[j].TotalScore)
                        result.Add(group1.Teams[i++]);
                    else
                        result.Add(group2.Teams[j++]);
                }
                while (i < size / 2)
                {
                    result.Add(group1.Teams[i++]);
                }
                while (j < size / 2)
                {
                    result.Add(group2.Teams[j++]);
                }
                return result;
            }

            public void Print()
            {
                Write($"{_name}: ");
                foreach (var team in _teams)
                {
                    team.Print();
                }
                WriteLine("");
            }
        }
    }
}