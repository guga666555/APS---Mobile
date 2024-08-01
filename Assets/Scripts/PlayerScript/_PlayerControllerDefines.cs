using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerConfig
{
    [CreateAssetMenu]
    public class _PlayerControllerDefines : ScriptableObject
    {
        [Header("Configura��es do Personagem")]
        [Tooltip("Define a velocidade do personagem")]
        public float _moveSpeed = 6f;

        [Header("Configura��es da C�mera")]
        [Tooltip("Define a sensibilidade X da c�mera")]
        public float _sensX = 90f;

        [Tooltip("Define a sensibilidade Y da c�mera")]
        public float _sensY = 90f;

        [Tooltip("Define a rota��o m�nima/inferior da c�mera")]
        public float _maxLowerRotation = 90f;

        [Tooltip("Define a rota��o m�xima/superior da c�mera")] 
        public float _maxUpperRotation = 90f;

       // [Header("Configura��es de Controle")]
       // [Tooltip("Define o bot�o de Intera��o")]
       // public KeyCode _interactionKey = KeyCode.F;

        [Header("Outras Configura��es")]
        [Tooltip("Define dist�ncia m�xima na qual o jogador pode interagir com objetos")]
        public float _interactionDistance;

        [Tooltip("Define a camada de objetos interativos")]
        public LayerMask _interactionLayer;

        [Tooltip("Define as camadas de objeto a serem utilizadas pelo sistema de f�sica")]
        public LayerMask _groundLayer;

        [Header("Define os sons de passo do personagem do jogador")]
        public AudioClip[] _fstepAudioRubber = default;
        public AudioClip[] _fstepAudioGrass = default;
        public AudioClip[] _fstepAudioMetal = default;
        public AudioClip[] _fstepAudioWood = default;
        public AudioClip[] _fstepAudioStairs = default;
        public AudioClip[] _fstepAudioAsphalt = default;
    }
}