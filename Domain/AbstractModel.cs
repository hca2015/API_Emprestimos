using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace API_Emprestimos.Models
{
    public abstract class AbstractModel
    {
        public virtual int getId()
        {
            var prCoEntity = this;
            int lEntityId;

            PropertyInfo key =
                prCoEntity.GetType()
                .GetProperties()
                .FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), true).Length != 0);

            lEntityId = key != null ? Convert.ToInt32(key.GetValue(prCoEntity, null)) : 0;

            return lEntityId;
        }

        public virtual void Update(AbstractModel abstractModel, bool prkey = false)
        {
            PropertyInfo[] propertiesPAR = abstractModel.GetType().GetProperties();
            PropertyInfo propertyTHIS;

            foreach (PropertyInfo propertyPAR in propertiesPAR)
            {
                if (prkey || propertyPAR.GetCustomAttributes(typeof(KeyAttribute), true).Length == 0)
                {
                    propertyTHIS = this.GetType().GetProperty(propertyPAR.Name);
                    if (propertyTHIS != null && propertyTHIS.CanWrite && propertyPAR.PropertyType.Name == propertyTHIS.PropertyType.Name)
                        propertyTHIS.SetValue(this, propertyPAR.GetValue(abstractModel, null), null);
                }
            }
        }
    }
}
