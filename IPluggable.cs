using System.Collections.Generic;

public interface IPluggable
{
    IEnumerable<DataTransferObject> Run(IEnumerable<DataTransferObject> data);
}
