# EnumFuncMap Example

The EnumFuncMap utility allows you to map enum values to specific functions, 
making it easier to manage and execute code based on enum states.

## Example

```
public enum MyEnum { Option1, Option2, Option3 }
public class MyEnumFuncMap 
{ 
    private EnumFuncMap<MyEnum, Action> _enumFuncMap;

    public MyEnumFuncMap()
    {
        _enumFuncMap = new EnumFuncMap<MyEnum, Action>(new Action[]
        {
            () => Debug.Log("Option 1 selected"),
            () => Debug.Log("Option 2 selected"),
            () => Debug.Log("Option 3 selected")
        });
    }

    public void SetEnum(MyEnum enumValue)
    {
        _enumFuncMap.Enum = enumValue;
        _enumFuncMap.Func();
    }
}
```