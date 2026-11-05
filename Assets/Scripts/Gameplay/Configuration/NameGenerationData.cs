using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Unity.BossRoom.Gameplay.Configuration
{
    /// <summary>
    /// Data storage of all the valid strings used to create a player's name.
    /// Currently names are a two word combination in Adjective-Noun Combo (e.g. Happy Apple)
    /// </summary>
    [CreateAssetMenu(menuName = "GameData/NameGeneration", order = 2)]
    public class NameGenerationData : ScriptableObject
    {
        [Tooltip("The list of all possible strings the game can use as the first word of a player name")]
        [SerializeField]
        [FormerlySerializedAs("FirstWordList")]
        string[] m_FirstWords;

        [Tooltip("The list of all possible strings the game can use as the second word in a player name")]
        [SerializeField]
        [FormerlySerializedAs("SecondWordList")]
        string[] m_SecondWords;

        public string GenerateName()
        {
            var firstWord = m_FirstWords[Random.Range(0, m_FirstWords.Length)];
            var secondWord = m_SecondWords[Random.Range(0, m_SecondWords.Length)];

            return firstWord + " " + secondWord;
        }
    }
}
