﻿using System.Collections.Generic;

namespace Vixen.Sys {
	class OutputIntentStateList : IntentStateList {
		public OutputIntentStateList(IEnumerable<IIntentState> intentStates)
			: base(intentStates) {
		}

		override public void AddIntentState(IIntentState intentState) {
			// Clone the intent state so that outputs using the same channels don't
			// use the same intent and clobber each other.
			Add(intentState.Clone());
		}
	}
	//class OutputIntentStateList : List<IIntentState>, IIntentStateList {
	//    public OutputIntentStateList(IEnumerable<IIntentState> intentStates) {
	//        foreach(IIntentState intentState in intentStates) {
	//            AddIntentState(intentState);
	//        }
	//    }

	//    public void AddIntentState(IIntentState intentState) {
	//        // Clone the intent state so that outputs using the same channels don't
	//        // use the same intent and clobber each other.
	//        IIntentState newIntentState = intentState.Clone();

	//        // Add.
	//        Add(newIntentState);
	//    }
		
	//    public void AddFilters(IEnumerable<IFilterState> filterStates) {
	//        if(VixenSystem.AllowFilterEvaluation) {
	//            foreach(IIntentState intentState in this) {
	//                intentState.FilterStates.AddRange(filterStates);
	//            }
	//        }
	//    }
	//}
}
