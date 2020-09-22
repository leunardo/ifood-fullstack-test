package com.ifood.demo.client;

import java.util.Collection;
import java.util.UUID;

import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.CrudRepository;
import org.springframework.data.repository.query.Param;
import org.springframework.data.rest.core.annotation.RestResource;


public interface ClientRepository extends CrudRepository<Client, UUID> {

	@RestResource(path = "byName")
	Collection<Client> findByNameIgnoreCaseContaining(@Param("name") String name);
	
	@RestResource(path = "byPhone")
	Collection<Client> findByPhoneIgnoreCaseContaining(@Param("phone") String phone);
	
	@RestResource(path = "byEmail")
	Collection<Client> findByEmailIgnoreCaseContaining(@Param("email") String email);

	@RestResource
	@Query(
		"SELECT c FROM Client c WHERE "
		+ "(:name is null or c.name like %:name%) and"
		+ "(:phone is null or c.phone like %:phone%) and"
		+ "(:email is null or c.email like %:email%)"
	)
	Collection<Client> findAllClients(
		@Param("name") String name,
		@Param("phone") String phone,
		@Param("email") String email
	);
}