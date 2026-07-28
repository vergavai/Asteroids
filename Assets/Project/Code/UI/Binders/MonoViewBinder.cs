using System;
using MVVM;
using UnityEditor;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Project.Code.UI.Binders
{
    public sealed class MonoViewBinder : MonoBehaviour
    {
        enum BindingMode
        {
            FromInstance = 0,
            FromResolve = 1,
            FromResolveId = 2
        }

        [SerializeField] private BindingMode viewBinding;
        [SerializeField] private Object view;
        [SerializeField] private MonoScript viewType;
        [SerializeField] private string viewId;

        [Space(8)]
        [SerializeField] private BindingMode viewModelBinding;
        [SerializeField] private Object viewModel;
        [SerializeField] private MonoScript viewModelType;
        [SerializeField] private string viewModelId;

        [Inject] 
        private DiContainer diContainer;

        private IBinder binder;

        private void Awake()
        {
            binder = CreateBinder();
        }

        private void OnEnable()
        {
            binder.Bind();
        }

        private void OnDisable()
        {
            binder.Unbind();
        }

        private IBinder CreateBinder()
        {
            object view = viewBinding switch
            {
                BindingMode.FromInstance => this.view,
                BindingMode.FromResolve => diContainer.Resolve(viewType.GetClass()),
                BindingMode.FromResolveId => diContainer.ResolveId(viewType.GetClass(), viewId),
                _ => throw new Exception($"Binding type of view {viewBinding} is not found!")
            };

            object model = viewModelBinding switch
            {
                BindingMode.FromInstance => viewModel,
                BindingMode.FromResolve => diContainer.Resolve(viewModelType.GetClass()),
                BindingMode.FromResolveId => diContainer.ResolveId(viewModelType.GetClass(), viewModelId),
                _ => throw new Exception($"Binding type of view {viewBinding} is not found!")
            };

            return BinderFactory.CreateComposite(view, model);
        }
    }
}
