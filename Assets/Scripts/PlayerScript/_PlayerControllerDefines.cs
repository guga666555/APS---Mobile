using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerConfig
{
    [CreateAssetMenu]
    public class _PlayerControllerDefines : ScriptableObject
    {
        [Header("Configurações do Personagem")]
        [Tooltip("Define a velocidade do personagem")]
        public float _moveSpeed = 6f;

        [Header("Configurações da Câmera")]
        [Tooltip("Define a sensibilidade X da câmera")]
        public float _sensX = 90f;

        [Tooltip("Define a sensibilidade Y da câmera")]
        public float _sensY = 90f;

        [Tooltip("Define a rotação mínima/inferior da câmera")]
        public float _maxLowerRotation = 90f;

        [Tooltip("Define a rotação máxima/superior da câmera")] 
        public float _maxUpperRotation = 90f;

       // [Header("Configurações de Controle")]
       // [Tooltip("Define o botão de Interação")]
       // public KeyCode _interactionKey = KeyCode.F;

        [Header("Outras Configurações")]
        [Tooltip("Define distância máxima na qual o jogador pode interagir com objetos")]
        public float _interactionDistance;

        [Tooltip("Define a camada de objetos interativos")]
        public LayerMask _interactionLayer;

        [Tooltip("Define as camadas de objeto a serem utilizadas pelo sistema de física")]
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