import React, { useState, useEffect } from 'react';

function App() {
    const [products, setProducts] = useState([]);
    const [newProduct, setNewProduct] = useState({ name: '', price: '' });
    const [editProduct, setEditProduct] = useState({ id: '', name: '', price: '' });

    useEffect(() => {
        fetch('http://localhost:5000/api/product')
            .then(response => response.json())
            .then(data => setProducts(data))
            .catch(error => console.error('Error fetching products:', error));
    }, []);

    // Додати новий продукт
    const handleAddProduct = () => {
        fetch('http://localhost:5000/api/product', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(newProduct)
        })
            .then(response => response.json())
            .then(data => {
                setProducts([...products, data]);  // Додаємо новий продукт до списку
                setNewProduct({ name: '', price: '' });  // Очищаємо форму
            })
            .catch(error => console.error('Error adding product:', error));
    };

    // Оновити продукт
    const handleEditProduct = (id) => {
        fetch(`http://localhost:5000/api/product/${id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(editProduct)
        })
            .then(response => {
                if (response.ok) {
                    setProducts(products.map(product =>
                        product.id === id ? { ...product, ...editProduct } : product
                    ));
                    setEditProduct({ id: '', name: '', price: '' });
                }
            })
            .catch(error => console.error('Error editing product:', error));
    };

    // Видалити продукт
    const handleDeleteProduct = (id) => {
        fetch(`http://localhost:5000/api/product/${id}`, {
            method: 'DELETE'
        })
            .then(response => {
                if (response.ok) {
                    setProducts(products.filter(product => product.id !== id));  // Видаляємо продукт з списку
                }
            })
            .catch(error => console.error('Error deleting product:', error));
    };

    return (
        <div className="App">
            <h1>Product List</h1>
            <ul>
                {products.map(product => (
                    <li key={product.id}>
                        {product.name} - ${product.price}
                        <button onClick={() => setEditProduct(product)}>Edit</button>
                        <button onClick={() => handleDeleteProduct(product.id)}>Delete</button>
                    </li>
                ))}
            </ul>

            <h2>Add Product</h2>
            <input
                type="text"
                placeholder="Product Name"
                value={newProduct.name}
                onChange={(e) => setNewProduct({ ...newProduct, name: e.target.value })}
            />
            <input
                type="number"
                placeholder="Product Price"
                value={newProduct.price}
                onChange={(e) => setNewProduct({ ...newProduct, price: e.target.value })}
            />
            <button onClick={handleAddProduct}>Add Product</button>

            <h2>Edit Product</h2>
            {editProduct.id && (
                <div>
                    <input
                        type="text"
                        placeholder="Product Name"
                        value={editProduct.name}
                        onChange={(e) => setEditProduct({ ...editProduct, name: e.target.value })}
                    />
                    <input
                        type="number"
                        placeholder="Product Price"
                        value={editProduct.price}
                        onChange={(e) => setEditProduct({ ...editProduct, price: e.target.value })}
                    />
                    <button onClick={() => handleEditProduct(editProduct.id)}>Save Changes</button>
                </div>
            )}
        </div>
    );
}

export default App;