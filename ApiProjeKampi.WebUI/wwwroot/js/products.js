const API_BASE_URL = 'https://localhost:7002/api'; // API base URL'inizi güncelleyin

// Sayfa yüklendiğinde çalışacak
document.addEventListener('DOMContentLoaded', function() {
    loadProducts();
    loadCategories();
});

// Ürünleri yükle
async function loadProducts() {
    try {
        const response = await fetch(`${API_BASE_URL}/Products/ProductListWithCategory`);
        const products = await response.json();
        
        const tbody = document.getElementById('productTableBody');
        tbody.innerHTML = '';
        
        products.forEach(product => {
            const row = `
                <tr>
                    <td>${product.productId}</td>
                    <td>${product.productName}</td>
                    <td>${product.description || '-'}</td>
                    <td>${product.price} ₺</td>
                    <td>${product.categoryName}</td>
                    <td>
                        <button class="btn btn-sm btn-warning me-1" onclick="editProduct(${product.productId})">
                            <i class="fas fa-edit"></i>
                        </button>
                        <button class="btn btn-sm btn-danger" onclick="deleteProduct(${product.productId})">
                            <i class="fas fa-trash"></i>
                        </button>
                    </td>
                </tr>
            `;
            tbody.innerHTML += row;
        });
    } catch (error) {
        console.error('Ürünler yüklenirken hata:', error);
        showAlert('Ürünler yüklenirken hata oluştu!', 'danger');
    }
}

// Kategorileri yükle
async function loadCategories() {
    try {
        const response = await fetch(`${API_BASE_URL}/Categories`);
        const categories = await response.json();
        
        const createSelect = document.getElementById('createCategoryId');
        const editSelect = document.getElementById('editCategoryId');
        
        createSelect.innerHTML = '<option value="">Kategori Seçin</option>';
        editSelect.innerHTML = '<option value="">Kategori Seçin</option>';
        
        categories.forEach(category => {
            const option = `<option value="${category.categoryId}">${category.categoryName}</option>`;
            createSelect.innerHTML += option;
            editSelect.innerHTML += option;
        });
    } catch (error) {
        console.error('Kategoriler yüklenirken hata:', error);
    }
}

// Yeni ürün oluştur
async function createProduct() {
    const productData = {
        productName: document.getElementById('createProductName').value,
        description: document.getElementById('createDescription').value,
        price: parseFloat(document.getElementById('createPrice').value),
        categoryId: parseInt(document.getElementById('createCategoryId').value),
        imageUrl: document.getElementById('createImageUrl').value
    };
    
    if (!productData.productName || !productData.price || !productData.categoryId) {
        showAlert('Lütfen zorunlu alanları doldurun!', 'warning');
        return;
    }
    
    try {
        const response = await fetch(`${API_BASE_URL}/Products/CreateProductWithCategory`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(productData)
        });
        
        if (response.ok) {
            showAlert('Ürün başarıyla eklendi!', 'success');
            document.getElementById('createForm').reset();
            bootstrap.Modal.getInstance(document.getElementById('createModal')).hide();
            loadProducts();
        } else {
            showAlert('Ürün eklenirken hata oluştu!', 'danger');
        }
    } catch (error) {
        console.error('Ürün ekleme hatası:', error);
        showAlert('Ürün eklenirken hata oluştu!', 'danger');
    }
}

// Ürün düzenle
async function editProduct(id) {
    try {
        const response = await fetch(`${API_BASE_URL}/Products/GetProduct?id=${id}`);
        const product = await response.json();
        
        document.getElementById('editProductId').value = product.productId;
        document.getElementById('editProductName').value = product.productName;
        document.getElementById('editDescription').value = product.description || '';
        document.getElementById('editPrice').value = product.price;
        document.getElementById('editCategoryId').value = product.categoryId;
        document.getElementById('editImageUrl').value = product.imageUrl || '';
        
        new bootstrap.Modal(document.getElementById('editModal')).show();
    } catch (error) {
        console.error('Ürün bilgileri alınırken hata:', error);
        showAlert('Ürün bilgileri alınırken hata oluştu!', 'danger');
    }
}

// Ürün güncelle
async function updateProduct() {
    const productData = {
        productId: parseInt(document.getElementById('editProductId').value),
        productName: document.getElementById('editProductName').value,
        description: document.getElementById('editDescription').value,
        price: parseFloat(document.getElementById('editPrice').value),
        categoryId: parseInt(document.getElementById('editCategoryId').value),
        imageUrl: document.getElementById('editImageUrl').value
    };
    
    try {
        const response = await fetch(`${API_BASE_URL}/Products`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(productData)
        });
        
        if (response.ok) {
            showAlert('Ürün başarıyla güncellendi!', 'success');
            bootstrap.Modal.getInstance(document.getElementById('editModal')).hide();
            loadProducts();
        } else {
            showAlert('Ürün güncellenirken hata oluştu!', 'danger');
        }
    } catch (error) {
        console.error('Ürün güncelleme hatası:', error);
        showAlert('Ürün güncellenirken hata oluştu!', 'danger');
    }
}

// Ürün sil
async function deleteProduct(id) {
    if (!confirm('Bu ürünü silmek istediğinizden emin misiniz?')) {
        return;
    }
    
    try {
        const response = await fetch(`${API_BASE_URL}/Products?id=${id}`, {
            method: 'DELETE'
        });
        
        if (response.ok) {
            showAlert('Ürün başarıyla silindi!', 'success');
            loadProducts();
        } else {
            showAlert('Ürün silinirken hata oluştu!', 'danger');
        }
    } catch (error) {
        console.error('Ürün silme hatası:', error);
        showAlert('Ürün silinirken hata oluştu!', 'danger');
    }
}

// Alert göster
function showAlert(message, type) {
    const alertDiv = document.createElement('div');
    alertDiv.className = `alert alert-${type} alert-dismissible fade show position-fixed`;
    alertDiv.style.cssText = 'top: 20px; right: 20px; z-index: 9999; min-width: 300px;';
    alertDiv.innerHTML = `
        ${message}
        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    `;
    
    document.body.appendChild(alertDiv);
    
    setTimeout(() => {
        if (alertDiv.parentNode) {
            alertDiv.parentNode.removeChild(alertDiv);
        }
    }, 5000);
}