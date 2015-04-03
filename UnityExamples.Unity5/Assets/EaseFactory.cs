using UnityEngine;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core.Easing;

public class EaseFactory {

	public static EaseFunction StopMotion(int motionFps, EaseFunction innerEase = null) {
		// Fallback
		if (innerEase == null)
			innerEase = StandardEaseFunction(DOTween.defaultEaseType);

		// Compute the time interval in which we must re-evaluate the value
		float motionDelay = 1.0f / motionFps;

		return delegate(float time, float duration, float overshootOrAmplitude, float period) {
			// Adjust the time so it's in steps
			float steptime = time - (time % motionDelay);

			// Evaluate the ease value based on the new step time
			return innerEase(steptime, duration, overshootOrAmplitude, period);
		};
	}

	// The easeType must not be INTERNAL_Custom
	public static EaseFunction StandardEaseFunction(Ease easeType)
	{
		return delegate(float time, float duration, float overshootOrAmplitude, float period) {
			return EaseManager.Evaluate(easeType, null, time, duration, overshootOrAmplitude, period);
		};
	}

}
