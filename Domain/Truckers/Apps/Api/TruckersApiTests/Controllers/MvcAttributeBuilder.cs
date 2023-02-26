namespace TruckersApiTests.Controllers
{
    public class MvcAttributeBuilder
    {
        public static TAttribute? ExtractAttributeFrom<TAttribute, TController>(string actionName)
            where TAttribute : Attribute
            where TController : class
        {
            var controllerType = typeof(TController);
            var attributeType = typeof(TAttribute);
            var methodInfo = controllerType.GetMethod(actionName);
            if (methodInfo is null) return null;

            var methodAttributes = methodInfo.GetCustomAttributes(true);
            // act
            return methodAttributes.FirstOrDefault(x => x.GetType() == attributeType) as TAttribute;
        }
    }
}
