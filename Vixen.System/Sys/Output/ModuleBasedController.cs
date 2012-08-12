﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Vixen.Sys.Output {
	abstract public class ModuleBasedController<T, U> : ModuleBasedOutputDevice<T> 
		where T : class, IOutputModule, IHasOutputs, IHardwareModule 
		where U : Output, new() {
		private OutputCollection<U> _outputs;

		protected ModuleBasedController(string name, int outputCount, Guid moduleId)
			: this(Guid.NewGuid(), name, outputCount, moduleId) {
		}

		protected ModuleBasedController(Guid id, string name, int outputCount, Guid moduleId)
			: base(id, name, moduleId) {
			_outputs = new OutputCollection<U>();
			_outputs.OutputAdded += OutputAdded;
			_outputs.OutputRemoved += OutputRemoved;
			OutputCount = outputCount;
		}

		protected virtual void OutputRemoved(object sender, OutputCollectionEventArgs<U> e) { }

		protected virtual void OutputAdded(object sender, OutputCollectionEventArgs<U> e) { }

		protected override T GetModule(Guid moduleId) {
			T module = GetControllerModule(moduleId);
			if(module != null) {
				_SetOutputModuleOutputCount(module);
			}
			return module;
		}

		abstract protected T GetControllerModule(Guid moduleId);

		protected void BeginOutputChange() {
			Monitor.Enter(_outputs);
		}

		protected void EndOutputChange() {
			Monitor.Exit(_outputs);
		}

		protected IEnumerable<V> ExtractFromOutputs<V>(Func<U,V> outputPropertySelector) {
			return _outputs.Select(outputPropertySelector);
		}

		private void _SetOutputModuleOutputCount(T module) {
			if(module != null && OutputCount != 0) {
				module.OutputCount = OutputCount;
			}
		}

		public U[] Outputs {
			get { return _outputs.AsArray; }
		}

		public int OutputCount {
			get { return _outputs.Count; }
			set {
				lock(_outputs) {
					_outputs.Count = value;
				}
				_SetOutputModuleOutputCount(Module);
			}
		}

		//public void AddSources(IOutputSourceCollection sources) {
		//    _outputs.AddSources(sources);
		//}

		//public void RemoveSources(IOutputSourceCollection sources) {
		//    _outputs.RemoveSources(sources);
		//}

		//public void ReloadSources() {
		//    _outputs.ReloadSources();
		//}

		//public void ReloadOutputSources(int outputIndex) {
		//    _outputs.ReloadOutputSources(outputIndex);
		//}

		//public void ClearSources(int outputIndex) {
		//    _outputs.ClearSources(outputIndex);
		//}
	}
}