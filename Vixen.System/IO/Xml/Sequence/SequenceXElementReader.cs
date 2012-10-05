﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;
using Vixen.Module.SequenceType;
using Vixen.Services;
using Vixen.Sys;

namespace Vixen.IO.Xml.Sequence {
	class SequenceXElementReader : IObjectContentReader {
		private string _fileType;

		public SequenceXElementReader(string filePath) {
			if(string.IsNullOrWhiteSpace(filePath)) throw new ArgumentNullException("filePath");

			_fileType = FileService.Instance.GetFileType(filePath);
		}

		public object ReadContentFromObject(object obj) {
			ISequence sequence = obj as ISequence;
			if(sequence == null) throw new InvalidOperationException("Object must be an ISequence.");

			return _GenerateSequenceDataContent(sequence);
		}

		private XElement _GenerateSequenceDataContent(ISequence sequence) {
			using(MemoryStream stream = new MemoryStream()) {
				using(XmlWriter xmlWriter = XmlWriter.Create(stream)) {
					_WriteSequenceDataToXmlWriter(sequence, xmlWriter);
				}
				return XElement.Load(stream);
			}
		}

		private void _WriteSequenceDataToXmlWriter(ISequence sequence, XmlWriter xmlWriter) {
			ISequenceTypeModuleInstance sequenceTypeModule = _GetSequenceTypeModule(_fileType);
			DataContractSerializer serializer = SequenceTypeService.GetSequenceTypeDataSerializer(sequenceTypeModule);
			if(serializer == null) throw new Exception("Can't save sequence " + sequence.Name + ", no serializer present.");

			serializer.WriteStartObject(xmlWriter, sequence.SequenceData);
			_WriteKnownNamespaces(xmlWriter);
			serializer.WriteObjectContent(xmlWriter, sequence.SequenceData);
			serializer.WriteEndObject(xmlWriter);
		}

		private ISequenceTypeModuleInstance _GetSequenceTypeModule(string fileType) {
			return SequenceTypeService.Instance.CreateSequenceFactory(fileType);
		}

		private void _WriteKnownNamespaces(XmlWriter xmlWriter) {
			List<string> namespaces = new List<string> {
				"http://www.w3.org/2001/XMLSchema"
			};

			int aliasIndex = 0;
			foreach(string ns in namespaces) {
				string alias = _GetAlias(aliasIndex++);
				xmlWriter.WriteAttributeString("xmlns", alias, null, ns);
			}
		}

		//http://stackoverflow.com/questions/297213/translate-an-index-into-an-excel-column-name
		private string _GetAlias(int aliasIndex) {
			int quotient = aliasIndex / 26;
			if(quotient > 0) {
				return _GetAlias(quotient - 1) + (char)(aliasIndex % 26 + 'a');
			} else {
				return ((char)(aliasIndex + 'a')).ToString();
			}
		}
	}
}
