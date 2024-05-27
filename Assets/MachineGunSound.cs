using System;
using System.Collections.Generic;
using AIBERG.Factions.TheOrderOfTheFlesh;
using UnityEngine;

namespace AIBERG
{
    public class MachineGunSound : MonoBehaviour
    {
        public AudioClip shootSound;
        [SerializeField] List<AudioSource> audioSources = new List<AudioSource>();
        [SerializeField] private int currentAudioSourceIndex;
        [SerializeField] private MachineGun machineGun;
        // Start is called before the first frame update
        void Start()
        {
            machineGun = GetComponent<MachineGun>();
            AudioSource[] audioSourceComponents = GetComponents<AudioSource>();
            foreach(AudioSource a in audioSourceComponents){
                audioSources.Add(a);
            }
            currentAudioSourceIndex = 0;
            if(machineGun != null){
                machineGun.OnProjectileShot += MachineGun_OnProjectileShot;
            }
        }

        private void MachineGun_OnProjectileShot(object sender, EventArgs e)
        {
            if(SoundManager.Instance != null){
                SoundManager.Instance.PlaySound(audioSources[currentAudioSourceIndex++], shootSound, 1f, 1.5f);
                if(currentAudioSourceIndex == audioSources.Count){
                    currentAudioSourceIndex = 0;
                }
            }
        }
    }
}
