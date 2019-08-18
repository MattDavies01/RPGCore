using NUnit.Framework;
using RPGCore.Inventory.Slots;
using RPGCore.Items;

namespace RPGCore.Inventories.UnitTests
{
	public class WeightedInventoryConstraintShould
	{
		[Test, Parallelizable]
		public void AllowWeightlessItems ()
		{
			var storageSlot = new ItemStorageSlot
			(
				new IInventoryConstraint[]
				{
					new WeightedInventoryConstraint(10)
				}
			);

			var template = new ProceduralItemTemplate ()
			{
				Weight = 0
			};

			var itemToAddA = new StackableItem (template, 250);
			var result = storageSlot.AddItem (itemToAddA);

			Assert.AreEqual (250, ((StackableItem)result.ItemAdded).Quantity);
		}

		[Test, Parallelizable]
		public void DeclineOverweightedItems ()
		{
			var storageSlot = new ItemStorageSlot
			(
				new IInventoryConstraint[]
				{
					new WeightedInventoryConstraint(10)
				}
			);

			var template = new ProceduralItemTemplate ()
			{
				Weight = 100
			};

			var itemToAdd = new StackableItem (template, 1);
			var result = storageSlot.AddItem (itemToAdd);

			Assert.AreEqual (0, result.Quantity);
			Assert.AreEqual (InventoryResult.OperationStatus.None, result.Status);
			Assert.AreEqual (null, result.ItemAdded);
		}

		[Test, Parallelizable]
		public void LimitCapacityInEmptyFilledItemStorageSlot ()
		{
			var storageSlot = new ItemStorageSlot
			(
				new IInventoryConstraint[]
				{
					new WeightedInventoryConstraint(5)
				}
			);

			var template = new ProceduralItemTemplate ()
			{
				Weight = 1
			};

			var itemToAddA = new StackableItem (template, 10);

			var result = storageSlot.AddItem (itemToAddA);

			Assert.AreEqual (5, ((StackableItem)result.ItemAdded).Quantity);
		}

		[Test, Parallelizable]
		public void LimitCapacityInFullItemStorageSlot ()
		{
			var storageSlot = new ItemStorageSlot
			(
				new IInventoryConstraint[]
				{
					new WeightedInventoryConstraint(3)
				}
			);

			var template = new ProceduralItemTemplate ()
			{
				Weight = 1
			};

			var itemToAddA = new StackableItem (template, 3);
			var itemToAddB = new StackableItem (template, 5);

			storageSlot.AddItem (itemToAddA);
			var result = storageSlot.AddItem (itemToAddB);

			Assert.AreEqual (0, result.Quantity);
			Assert.AreEqual (InventoryResult.OperationStatus.None, result.Status);
			Assert.AreEqual (null, result.ItemAdded);
		}

		[Test, Parallelizable]
		public void LimitCapacityInPartiallyFilledItemStorageSlot ()
		{
			var storageSlot = new ItemStorageSlot
			(
				new IInventoryConstraint[]
				{
					new WeightedInventoryConstraint(3)
				}
			);

			var template = new ProceduralItemTemplate ()
			{
				Weight = 1
			};

			var itemToAddA = new StackableItem (template, 2);
			var itemToAddB = new StackableItem (template, 2);

			storageSlot.AddItem (itemToAddA);
			var result = storageSlot.AddItem (itemToAddB);

			Assert.AreEqual (3, ((StackableItem)result.ItemAdded).Quantity);
		}
	}
}
