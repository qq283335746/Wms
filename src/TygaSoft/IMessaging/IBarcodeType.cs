using System;
using TygaSoft.Model;

namespace TygaSoft.IMessaging
{
    public interface IBarcodeType
    {
        BarcodeTypeInfo Receive();

        BarcodeTypeInfo Receive(int timeout);

        void Send(BarcodeTypeInfo model);
    }
}
