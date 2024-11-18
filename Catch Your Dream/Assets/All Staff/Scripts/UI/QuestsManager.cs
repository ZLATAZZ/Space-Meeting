using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestsManager : MonoBehaviour
{
    [Header("Quests for Aliens")]
    [SerializeField] private Image[] background;
    [SerializeField] private TextMeshProUGUI fact;

    [Header("All Audio Sources for Voice Mode")]
    [SerializeField] private AudioSource[] audioFacts;
    

    private int factsIndex;
    

   private Dictionary<int, string> facts = new Dictionary<int, string>()
        {
            [0] = "Do you know the first person, " +
            "who stepped onto the Moon? It was Neil Armstrong," +
            " who did it on 21 July 1969",

            [1] = "Do you sometimes consider that your human day lasts too long. But just imagine one Mercurian day lasts 59 Earth days!!!",

            [2] = "For planet Pluto it takes 248 year to finish its orbit, and its discovery was only in 1930…oh, it will really take time…",

            [3] = "Yuri Gagarin was first person, who was in space!! He made it on 12th April 1961",

            [4] = "The Sun is a star located at the center of our solar system",

            [5] = "Earth is the third planet from the Sun",

            [6] = "Do you know that Jupiter is the largest planet in our solar system. Its area is about 6,142E10 km2",

            [7] = "You know that some way contains our solar system? It's called Milky - Milky Way",

            [8] = "My friend, we even have red planet! Mars is known as the Red Planet because of its red environment",

            [9] = "I have another new fact for you - the Sun accounts for about 99.86% of the mass of the entire solar system",

            [10] = "Have you ever seen this bright and sometimes colorful comets? They are made of ice, dust, and rock!",

            [11] = "The Andromeda Galaxy is the closest spiral galaxy to the Milky Way",

            [12] = "Hey, friend, why no one can escape from Black Hole? Because Black holes have such strong gravity that not even light can escape them!",

            [13] = "Shhhh...space is completely silent, because there is no atmosphere to carry sound",

            [14] = "Our solar system is about 4.6 billion years old",

            [15] = "The planet Venus rotates in the opposite direction to most other planets. This means that on Venus the Sun " +
            "rises in the west and sets in the east",

            [16] = "The temperature on Venus can reach up to 465 degrees Celsius",

            [17] = "There are more than 100 billion galaxies in the observable universe",

            [18] = "Day is longer then year??? Whaaaat?? Yeah, Venus has a day longer than its year",

            [19] = "Neptune has the strongest winds in the solar system",

            [20] = "The closest star to Earth, other than the Sun, is Proxima Centauri",

            [21] = "Uranus spins sideways. It happens because of some sort of titanic collision in the ancient past",

            [22] = "Spacecraft have visited every planet in our solar system",

            [23] = "The light from the Sun takes about 8 minutes and 20 seconds to reach Earth",

            [24] = "Oh..here is so hot! The temperature on the surface of the Sun is about 5,500 degrees Celsius",

            [25] = "Astronauts' height can increase by up to 2 inches while in space due to the lack of gravity",

            [26] = "Trrrrr....earthquakes or moonquakes. Yeah, The Moon has quakes, called moonquakes, similar to Earth's earthquakes",

            [27] = "Hey! Wanna some new facts? The surface gravity on Mars is about 38% that of Earth's!",

            [28] = "Belt? Oh no, not the part of clothes. The asteroid belt lies between Mars and Jupiter",

            [29] = "Not only Earth has liquid on its surface. It was discovered that Mars also has it!!!",

            [30] = "Long trip? Oh, just imagine if you could fly a plane to Pluto, the trip would take more than 800 years!"
   }; 
  

    public static QuestsManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

   
    public void ActivateFact(int questionIndex)
    {
        //set appropriate background
        background[questionIndex].gameObject.SetActive(true);

        //set random facts
        factsIndex = Random.Range(0, facts.Count);

        string currentFact = facts[factsIndex];

        fact.text = currentFact;
        if (SceneFlow.Instance.isVoiceModeOn)
        {
            audioFacts[factsIndex].Play();
            StartCoroutine(WaitForTheEnd());

        }
        
    }


    public void StopVoiceFacts()
    {
        audioFacts[factsIndex].Stop();
    }

    public void GetTheAnswer(string response)
    {
        if(response.ToLower().Contains("Ok") || response.ToLower().Contains("ok") || response.ToLower().Contains("a"))
        {
            GameFollow.Instance.DestroyAlien();
            GameFollow.Instance.ResumeGame();
            foreach(var bg in background)
            {
                bg.gameObject.SetActive(false);
            }
            Debug.Log("we say yes");
        }
    }

    IEnumerator WaitForTheEnd()
    {
        while (audioFacts[factsIndex].isPlaying)
        {
            yield return null;
        }
        VoiceMovement.Instance.StartRecording();
       
    }

}
