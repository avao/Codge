using Codge.DataModel.Descriptors;
using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codge.DataModel.Descriptors.Serialisation;

namespace Codge.DataModel.Framework
{
    public class ModelMergeTypeSystemEventHandler
            : NamespaceTracingTypeSystemEventHandler<NamespaceDescriptor>
            , IAtomicNodeEnventHandler<PrimitiveTypeDescriptor>
            , IAtomicNodeEnventHandler<CompositeTypeDescriptor>
            , IAtomicNodeEnventHandler<EnumerationTypeDescriptor>
    {
        private readonly ILog _logger;

        public ModelMergeTypeSystemEventHandler(NamespaceDescriptor ns, ILog logger)
            : base(ns, (n, descriptor) => n.GetOrCreateNamespace(descriptor.Name))
        {
            _logger = logger;
        }


        public void Handle(PrimitiveTypeDescriptor primitive)
        {
            var lhsType = Namespace.Types.FindByName(primitive.Name);
            if (lhsType == null)
            {
                Namespace.CreatePrimitiveType(primitive.Name);
            }
        }

        public void Handle(CompositeTypeDescriptor composite)
        {
            var lhsType = Namespace.Types.FindByName(composite.Name) as CompositeTypeDescriptor;
            if (lhsType == null)
            {
                lhsType = Namespace.CreateCompositeType(composite.Name);
            }

            foreach (var field in composite.Fields)
            {
                var lhsField = lhsType.Fields.FirstOrDefault(_ => _.Name == field.Name);
                if (lhsField == null)
                {
                    //TODO fields are apended, think about proper merge
                    lhsField = lhsType.AddField(field.Name, field.TypeName, field.IsCollection);
                }

                if (lhsField.IsCollection != field.IsCollection || lhsField.TypeName != field.TypeName)
                    _logger.WarnFormat("different field definitions lhs:[{0}], rhs:[{1}]", lhsField.ToXml(), field.ToXml());

                field.AttachedData.Where(_ => !lhsField.AttachedData.Keys.Contains(_.Key)).ToList().ForEach(_ => lhsField.AttachedData.Add(_));
            }
        }


        public void Handle(EnumerationTypeDescriptor enumeration)
        {
            var lhsType = Namespace.Types.FindByName(enumeration.Name) as EnumerationTypeDescriptor;
            if (lhsType == null)
            {
                lhsType = Namespace.CreateEnumerationType(enumeration.Name);
            }

            //TODO preserve position?
            foreach (var item in enumeration.Items)
            {
                var lhsItem = lhsType.Items.FirstOrDefault(_ => _.Name == item.Name);
                if (lhsItem == null)
                {
                    if (item.Value.HasValue)
                        lhsType.AddItem(item.Name, item.Value.Value);
                    else
                        lhsType.AddItem(item.Name);
                }
                else
                {
                    _logger.WarnFormat("different item definitions lhs:[{0}], rhs:[{1}]", lhsItem.ToXml(), item.ToXml());
                }
            }
        }
    }

}
