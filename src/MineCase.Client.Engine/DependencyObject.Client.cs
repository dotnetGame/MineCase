using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Engine
{
    public partial class DependencyObject
    {
        /// <inheritdoc/>
        protected override void Awake()
        {
            base.Awake();

            _valueStorage = new Data.DependencyValueStorage();
            _valueStorage.CurrentValueChanged += ValueStorage_CurrentValueChanged;
            InitializeComponents();
        }

        /// <summary>
        /// 初始化组件
        /// </summary>
        protected virtual void InitializeComponents()
        {
        }

        private void Update()
        {
            Tell(Messages.Update.Default);
        }

        private void OnAnimatorIK(int layerIndex)
        {
            Tell(new Messages.OnAnimatorIK { LayerIndex = layerIndex });
        }
    }
}
