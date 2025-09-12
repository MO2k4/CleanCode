using System;

// This class should trigger too many dependencies highlighting
public class ServiceWithTooManyDependencies
{
    private readonly IService1 _service1;
    private readonly IService2 _service2;
    private readonly IService3 _service3;
    private readonly IService4 _service4;
    private readonly IService5 _service5;
    private readonly IService6 _service6;
    private readonly IService7 _service7;
    private readonly IService8 _service8;
    private readonly IService9 _service9;
    private readonly IService10 _service10;

    public ServiceWithTooManyDependencies(
        IService1 service1,
        IService2 service2,
        IService3 service3,
        IService4 service4,
        IService5 service5,
        IService6 service6,
        IService7 service7,
        IService8 service8,
        IService9 service9,
        IService10 service10
    )
    {
        _service1 = service1;
        _service2 = service2;
        _service3 = service3;
        _service4 = service4;
        _service5 = service5;
        _service6 = service6;
        _service7 = service7;
        _service8 = service8;
        _service9 = service9;
        _service10 = service10;
    }
}

// This should be fine
public class ServiceWithFewDependencies
{
    private readonly IService1 _service1;
    private readonly IService2 _service2;

    public ServiceWithFewDependencies(IService1 service1, IService2 service2)
    {
        _service1 = service1;
        _service2 = service2;
    }
}

public interface IService1 { }

public interface IService2 { }

public interface IService3 { }

public interface IService4 { }

public interface IService5 { }

public interface IService6 { }

public interface IService7 { }

public interface IService8 { }

public interface IService9 { }

public interface IService10 { }
