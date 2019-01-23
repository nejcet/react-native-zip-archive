using System;
using System.IO.Compression;
using ReactNative.Bridge;
using ReactNative.Modules.Core;
using System.Threading.Tasks;

namespace RNZipArchive
{
    public class RNZipArchiveModule: ReactContextNativeModuleBase
    {
        public RNZipArchiveModule(ReactContext reactContext)
            : base(reactContext)
        {
        }

        private RCTNativeAppEventEmitter _emitter;

        public override string Name
        {
            get
            {
                return "RNZipArchive";
            }
        }

        internal RCTNativeAppEventEmitter Emitter
        {
            get
            {
                if (_emitter == null)
                {
                    return Context.GetJavaScriptModule<RCTNativeAppEventEmitter>();
                }

                return _emitter;
            }
            set
            {
                _emitter = value;
            }
        }

        [ReactMethod]
        public async void unzip(string source, string target, IPromise promise)
        {
            try
            {
                await Task.Run(() => ZipFile.ExtractToDirectory(source, target) ).ConfigureAwait(false);
                promise.Resolve(null);
            }
            catch (Exception ex)
            {
                promise.Reject(ex);
            }
        }

        [ReactMethod]
        public async void zip(string source, string target, IPromise promise)
        {
            try
            {
                await Task.Run(() => ZipFile.CreateFromDirectory(source, target)).ConfigureAwait(false);
                promise.Resolve(null);
            }
            catch (Exception ex)
            {
                promise.Reject(ex);
            }
        }

    }
}
