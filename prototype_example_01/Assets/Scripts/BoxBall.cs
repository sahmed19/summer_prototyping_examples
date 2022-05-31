using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class BoxBall : MonoBehaviour
{
    public Rigidbody2D Rigidbody;
    public float InitialSpeed;
    public float SpeedBonusPerBounce;
    public int NumBounces = 0;
    public CinemachineImpulseSource ImpulseSource;
    public ParticleSystem ImpactParticles;
    public AnimationCurve SquashStretchAnimationCurve;
    public AnimationCurve TimeDilationAnimationCurve;
    public AudioSource AudioSource;
    public bool RenderEffects = true;

    float mSquashStretchFactor = 1.0f; // 0 is squashed, 2 is stretched, 1 is normal
    Vector2 mDirection;

    void Start()
    {
        // set direction to random direction
        mDirection = Random.insideUnitCircle.normalized;
        NumBounces = 0;
    }

    public float GetSpeed()
    {
        return InitialSpeed + SpeedBonusPerBounce * NumBounces;
    }

    void Update()
    {
        UpdateScale();
    }

    void FixedUpdate()
    {
        float speed = GetSpeed();
        
        // distance = time x speed x direction
        Vector2 displacement = Time.fixedDeltaTime * speed * mDirection;
        Vector2 targetPosition = Rigidbody.position + displacement;
        
        // move rigidbody by appropriate distance
        Rigidbody.MovePosition(targetPosition);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // if hit border left or right
        if (other.gameObject.CompareTag("Border-X"))
        {
            // flip x direction
            mDirection.x *= -1.0f;
            NumBounces++;
            PlayImpactFX();
        } else if (other.gameObject.CompareTag("Border-Y"))
        {
            // flip y direction
            mDirection.y *= -1.0f;
            NumBounces++;
            PlayImpactFX();
        }
        
        // trig to make the box face direction of bounce
        transform.localRotation = Quaternion.Euler(Vector3.forward * Mathf.Rad2Deg * Mathf.Atan2(mDirection.y, mDirection.x));
    }

    void UpdateScale()
    {
        if (mSquashStretchFactor < 1.0f)
        {
            float t = mSquashStretchFactor;
            transform.localScale = .5f * Vector3.Lerp(
                new Vector3(0.2f, 1.8f, 0.0f),
                Vector3.one,
                t);
        } else if (mSquashStretchFactor > 1.0f)
        {
            float t = mSquashStretchFactor - 1.0f;
            transform.localScale = .5f * Vector3.Lerp(
                Vector3.one,
                new Vector3(1.8f, 0.2f, 0.0f),
                t);
        }
        else
        {
            transform.localScale = .5f * Vector3.one;
        }

    }

    IEnumerator mImpactFXRoutine;
    
    void PlayImpactFX()
    {
        if (!RenderEffects) return;
        
        if(mImpactFXRoutine != null)
            StopCoroutine(mImpactFXRoutine);
        mImpactFXRoutine = CR_PlayImpactFX();
        StartCoroutine(mImpactFXRoutine);
    }

    IEnumerator CR_PlayImpactFX()
    {
        //particles
        ImpactParticles.Play();
        // screen shake
        ImpulseSource.GenerateImpulseWithVelocity(mDirection);
        // sound
        AudioSource.pitch = Random.Range(.8f, 1.2f);
        AudioSource.Play();

        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * 2.0f;
            float squashStretchCurveEval = SquashStretchAnimationCurve.Evaluate(t);
            mSquashStretchFactor = squashStretchCurveEval;

            float timeDilationCurveEval = TimeDilationAnimationCurve.Evaluate(t);
            Time.timeScale = timeDilationCurveEval;
            yield return null;
        }

        Time.timeScale = 1.0f;
        mSquashStretchFactor = 1.0f;
    }
    
    
}
