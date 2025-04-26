<template>
  <div class="show w-100 vh-100 bg-white position-fixed translate-middle top-50 start-50 d-flex align-items-center justify-content-center">
    <div class="spinner-grow text-primary" role="status"></div>
  </div>

  <!-- Navbar Start -->
  <div class="container-fluid fixed-top shadow-sm bg-white">
    <div class="container px-0">
      <nav class="navbar navbar-light bg-white navbar-expand-xl">
        <router-link to="/" class="navbar-brand">
          <h1 class="text-primary display-6">BooKing Cinema</h1>
        </router-link>
        <button class="navbar-toggler py-2 px-3" type="button">
          <span class="fa fa-bars text-primary"></span>
        </button>

        <div class="collapse navbar-collapse bg-white">
          <div class="navbar-nav mx-auto">
            <router-link to="/" class="nav-item nav-link">Home</router-link>
            <router-link to="/products" class="nav-item nav-link">Movies</router-link>
            <router-link to="/cinemas" class="nav-item nav-link">Cinemas</router-link>

            <!-- Dropdown Thể Loại -->
            <div class="nav-item dropdown">
              <a class="nav-link dropdown-toggle" href="#st" role="button" data-bs-toggle="dropdown">
                Thể loại
              </a>
              <div class="dropdown-menu">
                <router-link
                  v-for="genre in genres"
                  :key="genre.id"
                  class="dropdown-item"
                  :to="`/Movies/genre/${genre.name}`"
                >
                  {{ genre.name }}
                </router-link>
              </div>
            </div>

            <router-link to="/login" class="nav-item nav-link">Login</router-link>
          </div>

          <div class="d-flex m-3 me-0">
            <button class="btn-search btn border border-secondary btn-md-square rounded-circle bg-white me-4">
              <i class="fas fa-search text-primary"></i>
            </button>
            <div class="position-relative me-4 my-auto">
              <i class="fa fa-shopping-bag fa-2x"></i>
              <span
                class="position-absolute bg-secondary rounded-circle d-flex align-items-center justify-content-center text-dark px-1"
                style="top: -5px; left: 15px; height: 20px; min-width: 20px;">
              </span>
            </div>

            <div v-if="user" class="d-flex align-items-center">
              <span class="me-2">Welcome, {{ user.firstName }} {{ user.lastName }}</span>
              <button @click="logout" class="btn btn-outline-danger btn-sm">Logout</button>
            </div>
          </div>
        </div>
      </nav>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import ProductService from '../../../service/productService';

const user = ref(null);
const genres = ref([]);
const router = useRouter();
onMounted(async () => {
  const storedUser = localStorage.getItem('user');
  if (storedUser) {
    user.value = JSON.parse(storedUser);
  }

  try {
    const response = await ProductService.getAllGenre();
    const genreData = response?.data;
    if (Array.isArray(genreData)) {
      genres.value = genreData.map((name, index) => ({
        id: index + 1,
        name: name.trim(),
      }));
    } else {
      console.error('Invalid genre data:', genreData);
    }
  } catch (error) {
    console.error('Error fetching genres:', error);
  }
});


const logout = () => {
  localStorage.removeItem('user');
  user.value = null;
  router.push('/login');
};
</script>

<style scoped>
.navbar-nav .nav-link.active {
  font-weight: bold;
  color: #007bff !important;
}

.header {
  transition: all 0.3s ease-in-out;
}

.header.scrolled {
  background-color: rgba(255, 255, 255, 0.9);
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
  padding: 5px 0;
}

.banner-container {
  margin-top: 85px;
}

.text-primary {
  color: #007bff !important;
}

.fw-bold {
  font-weight: 700;
}
</style>
