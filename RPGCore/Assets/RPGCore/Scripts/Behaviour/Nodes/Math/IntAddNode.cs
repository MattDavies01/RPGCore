﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGCore
{
	[NodeInformation ("Int/Add")]
	public class IntAddNode : BehaviourNode
	{
		public IntInput ValueA;
		public IntInput ValueB;

		public IntOutput Output;

		protected override void OnSetup (IBehaviourContext character)
		{
			ConnectionEntry<int> valueAInput = ValueA.GetEntry (character);
			ConnectionEntry<int> valueBInput = ValueB.GetEntry (character);
			ConnectionEntry<int> output = Output.GetEntry (character);

			Action updateHandler = () =>
			{
				output.Value = valueAInput.Value + valueBInput.Value;
			};

			valueAInput.OnAfterChanged += updateHandler;
			valueBInput.OnAfterChanged += updateHandler;

			updateHandler ();
		}

		protected override void OnRemove (IBehaviourContext character)
		{
		}

#if UNITY_EDITOR
		public override Vector2 GetDiamentions ()
		{
			return new Vector2 (140, 54);
		}
#endif
	}
}